using Anyex.Survey.Application.Services.SurveyResults.Dtos;

namespace Anyex.Survey.Application.Services.SurveyResults
{
    public interface ISurveyResultsService
    {
        Task<bool> VerifyPinCode(string surveyId, string? pinCode = null);
        Task<SurveyResultDto> GetSurveyResultsAsync(string surveyId);
    }
}