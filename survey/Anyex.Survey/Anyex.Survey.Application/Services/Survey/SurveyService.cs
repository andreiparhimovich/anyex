using Anyex.Survey.Application.DataAccess;
using Anyex.Survey.Application.Exceptions;
using Anyex.Survey.Application.Services.Survey.Converters;
using Anyex.Survey.Application.Services.Survey.Dtos;
using Anyex.Survey.Application.Services.Survey.Dtos.CreateSurvey;
using Anyex.Survey.Application.Services.Survey.Dtos.SurveyToSubmit;
using Anyex.Survey.Application.Services.Survey.Validators;

namespace Anyex.Survey.Application.Services.Survey;

public class SurveyService : ISurveyService
{
    private readonly IRepository<Domain.Survey, string> _surveyRepository;

    public SurveyService(IRepository<Domain.Survey, string> surveyRepository)
    {
        _surveyRepository = surveyRepository;
    }

    public async Task<SurveyToSubmitResponseDto> GetSurveyToSubmitAsync(string id)
    {
        var survey = await _surveyRepository.GetByIdAsync(id);

        if (survey is null)
        {
            throw new ApplicationNotFoundException($"The survey with id {id} is not found");
        }

        //
        // check if the survey is expired
        //
        if (survey.IsExpired)
        {
            return new SurveyToSubmitResponseDto
            {
                SurveyId = id,
                IsExpired = true
            };
        }

        var questionDtoConverter = new QuestionDtoConverter();

        var questions = survey.Questions
            .Select(x => questionDtoConverter.ToDtoModel(x))
            .ToList();

        return new SurveyToSubmitResponseDto
        {
            SurveyId = id,
            Title = survey.Title,
            IsExpired = false,
            Description = survey.Description,
            Questions = questions
        };
    }

    public async Task<CreateSurveyResponseDto> CreateSurveyAsync(CreateSurveyDto surveyDto)
    {
        var createSurveyDtoValidator = new CreateSurveyDtoValidator();

        var validationResult = await createSurveyDtoValidator.ValidateAsync(surveyDto);

        if (!validationResult.IsValid)
        {
            throw new ApplicationValidationException("Survey is not valid");
        }

        var questionDtoConverter = new QuestionDtoConverter();
        var surveyQuestions = surveyDto.Questions
            .Select(x => questionDtoConverter.ToDomainModel(x))
            .ToList();

        DateTime? expirationDateUtc = surveyDto.UseExpirationDate ? surveyDto.ExpirationDateUtc : null;

        var survey = Domain.Survey.Create(surveyDto.Title, surveyDto.Description, expirationDateUtc,
            surveyDto.ResultsAccessType, surveyDto.PinCode, surveyQuestions);

        var savedSurvey = await _surveyRepository.CreateAsync(survey);

        return new CreateSurveyResponseDto
        {
            SurveyId = savedSurvey.Id
        };
    }

    public async Task SubmitAnswersAsync(string surveyId, List<AnswerDto> answers)
    {
        Domain.Survey? survey = await _surveyRepository.GetByIdAsync(surveyId);

        if (survey is null)
        {
            throw new ApplicationNotFoundException($"There is no survey with id {surveyId}");
        }

        var answerValidator = new AnswerDtoValidator();

        foreach (var answer in answers)
        {
            var validationResult = await answerValidator.ValidateAsync(answer);
            if (!validationResult.IsValid)
            {
                throw new ApplicationValidationException(
                    $"Answer with for question with index = {answer.QuestionIndexNumber} for survey id = {surveyId} is not valid to be submitted");
            }
        }

        var answerDtoConverter = new AnswerDtoConverter();
        var answersToSubmit = answers.Select(x => answerDtoConverter.ToDomainModel(x)).ToList();

        survey.Submit(answersToSubmit);

        await _surveyRepository.UpdateAsync(survey);
    }
}