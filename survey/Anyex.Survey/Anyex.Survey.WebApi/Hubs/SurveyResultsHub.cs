using Anyex.Survey.Application.Services.SurveyResults;
using Anyex.Survey.Application.Services.SurveyResults.Dtos;
using Microsoft.AspNetCore.SignalR;

namespace Anyex.Survey.WebApi.Hubs;

public class SurveyResultsHub: Hub
{
    private readonly ISurveyResultsService _surveyResultsService;
    
    public SurveyResultsHub(ISurveyResultsService surveyResultsService)
    {
        _surveyResultsService = surveyResultsService;
    }
    
    public async Task AssociateConnectionWithSurveyId(string surveyId, string? pinCode)
    {
        //
        // check if we can add listener by verifying pin code
        //
        if (await _surveyResultsService.VerifyPinCode(surveyId, pinCode))
        {
            //
            // Use survey id as group name to store connections
            //
            await Groups.AddToGroupAsync(Context.ConnectionId, surveyId);
        }
    }
    
    public async Task SurveySubmitted(SurveyResultDto surveyResults)
    {
        var groupName = surveyResults.SurveyId!;

        await Clients.Group(groupName).SendAsync("SurveySubmitted", surveyResults);
    }
}