using Anyex.Survey.Domain.Questions;

namespace Anyex.Survey.Application.Services.Survey.Dtos;

public class AnswerDto
{
    public int QuestionIndexNumber { get; set; }
    public QuestionType QuestionType { get; set; }
    public SelectedOptionDto? SelectedOption { get; set; }
    public List<SelectedOptionDto> SelectedOptions { get; set; } = new();
    public int Rating { get; set; }
    public string? Text { get; set; }
}