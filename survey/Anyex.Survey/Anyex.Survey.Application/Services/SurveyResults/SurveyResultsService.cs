using Anyex.Survey.Application.DataAccess;
using Anyex.Survey.Application.Exceptions;
using Anyex.Survey.Application.Services.SurveyResults.Converters;
using Anyex.Survey.Application.Services.SurveyResults.Dtos;

namespace Anyex.Survey.Application.Services.SurveyResults;

public class SurveyResultsService : ISurveyResultsService
{
    private readonly IRepository<Domain.Survey, string> _surveyRepository;

    public SurveyResultsService(IRepository<Domain.Survey, string> surveyRepository)
    {
        _surveyRepository = surveyRepository;
    }

    public async Task<bool> VerifyPinCode(string surveyId, string? pinCode = null)
    {
        Domain.Survey? survey = await _surveyRepository.GetByIdAsync(surveyId);

        if (survey is null)
        {
            throw new ApplicationNotFoundException($"There is no survey with id {surveyId}");
        }

        return survey.VerifyPinCode(pinCode);
    }

    public async Task<SurveyResultDto> GetSurveyResultsAsync(string surveyId)
    {
        Domain.Survey? survey = await _surveyRepository.GetByIdAsync(surveyId);

        if (survey is null)
        {
            throw new ApplicationNotFoundException($"There is no survey with id {surveyId}");
        }

        var surveyResult = survey.GetResults();
            
        var questionResultDtoConverter = new QuestionResultDtoConverter();

        var questionResultsDto = surveyResult.QuestionResults
            .Select(result => questionResultDtoConverter.ToDtoModel(result))
            .ToList();
        
        return new SurveyResultDto
        {
            SurveyId = surveyResult.SurveyId,
            SurveyTitle = survey.Title,
            NumberOfSubmits = surveyResult.NumberOfSubmits,
            Results = questionResultsDto
        };
    }
}