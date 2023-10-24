using Anyex.Survey.Application.Exceptions;
using Anyex.Survey.Application.Services.Survey;
using Anyex.Survey.Application.Services.Survey.Dtos;
using Anyex.Survey.Application.Services.Survey.Dtos.CreateSurvey;
using Anyex.Survey.Application.Services.Survey.Dtos.SurveyToSubmit;
using Anyex.Survey.Application.Services.SurveyResults;
using Anyex.Survey.WebApi.Hubs;
using Anyex.Survey.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Anyex.Survey.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SurveyController : ControllerBase
{
    private readonly IHubContext<SurveyResultsHub> _surveyResultsHubContext;
    private readonly ISurveyService _surveyService;
    private readonly ISurveyResultsService _surveyResultsService;

    public SurveyController(
        ISurveyService surveyService, 
        IHubContext<SurveyResultsHub> surveyResultsHubContext, 
        ISurveyResultsService surveyResultsService)
    {
        _surveyService = surveyService;
        _surveyResultsHubContext = surveyResultsHubContext;
        _surveyResultsService = surveyResultsService;
    }

    [HttpGet("{surveyId}")]
    public async Task<ActionResult<SurveyToSubmitResponseDto>> GetSurveyToSubmit(string surveyId)
    {
        try
        {
            var survey = await _surveyService.GetSurveyToSubmitAsync(surveyId);

            return Ok(survey);
        }
        catch (ApplicationNotFoundException)
        {
            return NotFound();
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("create")]
    public async Task<ActionResult<CreateSurveyResponseModel>> CreateSurvey([FromBody] CreateSurveyDto survey)
    {
        try
        {
            var createSurveyResult = await _surveyService.CreateSurveyAsync(survey);

            var createSurveyResultModel = new CreateSurveyResponseModel
            {
                SurveyId = createSurveyResult.SurveyId,
                SurveyUrl = $"/survey/{createSurveyResult.SurveyId}",
                SurveyResultsUrl = $"/survey/{createSurveyResult.SurveyId}/results"
            };

            return Ok(createSurveyResultModel);
        }
        catch (ApplicationValidationException)
        {
            return BadRequest();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("{surveyId}/submit")]
    public async Task<ActionResult> SubmitAnswers(string surveyId, [FromBody] List<AnswerDto> answers)
    {
        try
        {
            await _surveyService.SubmitAnswersAsync(surveyId, answers);

            var surveyResults = await _surveyResultsService.GetSurveyResultsAsync(surveyId);

            await _surveyResultsHubContext.Clients.Group(surveyId).SendAsync("SurveySubmitted", surveyResults);

            return Ok();
        }
        catch (ApplicationNotFoundException)
        {
            return NotFound();
        }
        catch (ApplicationValidationException)
        {
            return BadRequest();
        }
        catch (Exception e)
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}