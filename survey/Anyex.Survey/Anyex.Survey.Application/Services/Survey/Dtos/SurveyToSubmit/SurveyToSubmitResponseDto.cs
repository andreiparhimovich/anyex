namespace Anyex.Survey.Application.Services.Survey.Dtos.SurveyToSubmit;

public class SurveyToSubmitResponseDto
{
    public string? SurveyId { get; set; }
    public string? Title { get; set; }
    public bool IsExpired { get; set; }
    public string? Description { get; set; }
    public List<QuestionDto> Questions { get; set; } = new();
}