using Anyex.Survey.Application.Services.Survey.Dtos;
using Anyex.Survey.Application.Services.Survey.Dtos.CreateSurvey;
using Anyex.Survey.Application.Services.Survey.Dtos.SurveyToSubmit;
using Anyex.Survey.Domain.SurveyResults;

namespace Anyex.Survey.Application.Services.Survey;

public interface ISurveyService
{
    Task<SurveyToSubmitResponseDto> GetSurveyToSubmitAsync(string id);
    Task<CreateSurveyResponseDto> CreateSurveyAsync(CreateSurveyDto surveyDto);
    Task SubmitAnswersAsync(string surveyId, List<AnswerDto> answers);
}
