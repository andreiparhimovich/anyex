using Anyex.Survey.Domain;

namespace Anyex.Survey.Application.Services.Survey.Dtos.CreateSurvey;

public class CreateSurveyDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool UseExpirationDate { get; set; }
    public DateTime ExpirationDateUtc { get; set; }
    public SurveyResultsAccessType ResultsAccessType { get; set; }
    public string? PinCode { get; set; }

    public List<QuestionDto> Questions { get; set; } = new();
}
