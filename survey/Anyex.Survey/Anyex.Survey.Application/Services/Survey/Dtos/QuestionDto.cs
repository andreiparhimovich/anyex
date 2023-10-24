using Anyex.Survey.Domain.Questions;

namespace Anyex.Survey.Application.Services.Survey.Dtos;

public class QuestionDto
{
    public int IndexNumber { get; set; }
    public string? Text { get; set; }
    public QuestionType QuestionType { get; set; }
    public List<QuestionOptionDto> Options { get; set; } = new();
    public int MaxRating { get; set; }
}
