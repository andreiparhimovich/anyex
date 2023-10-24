namespace Anyex.Survey.Application.Services.SurveyResults.Dtos;

public class SurveyResultDto
{
    public string? SurveyId { get; set; }

    public string? SurveyTitle { get; set; }

    public int NumberOfSubmits { get; set; }

    public List<QuestionResultDto> Results { get; set; } = new();
}