namespace Anyex.Survey.Application.Services.SurveyResults.Dtos;

public class QuestionOptionResultDto
{
    public int IndexNumber { get; set; }
    public string? OptionText { get; set; }
    public double Percentage { get; set; }
    public int NumberOfAnswers { get; set; }
}