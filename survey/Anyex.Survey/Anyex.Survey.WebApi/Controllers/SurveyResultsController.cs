using Anyex.Survey.Application.Exceptions;
using Anyex.Survey.Application.Services.SurveyResults;
using Anyex.Survey.Application.Services.SurveyResults.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Anyex.Survey.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SurveyResultsController : ControllerBase
{
    private readonly ISurveyResultsService _surveyResultsService;
    
    public SurveyResultsController(ISurveyResultsService surveyResultsService)
    {
        _surveyResultsService = surveyResultsService;
    }
    
    [HttpGet("{surveyId}")]
    public async Task<ActionResult<SurveyResultDto>> GetResults(string surveyId, [FromQuery] string? pinCode)
    {
        try
        {
            if (await _surveyResultsService.VerifyPinCode(surveyId, pinCode))
            {
                return await _surveyResultsService.GetSurveyResultsAsync(surveyId);   
            }

            return StatusCode(StatusCodes.Status401Unauthorized);
        }
        catch (ApplicationNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}