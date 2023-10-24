using Anyex.Survey.Domain.Questions;

namespace Anyex.Survey.Application.Services.SurveyResults.Dtos;

public class QuestionResultDto
{
    public QuestionType QuestionType { get; set; }
    public int IndexNumber { get; set; }
    public string? QuestionText { get; set; }
    public int NumberOfAnswers { get; set; }

    /// <summary>
    /// This field is average for Rating question type
    /// </summary>
    public double AverageRating { get; set; }

    /// <summary>
    /// Max value for Rating
    /// </summary>
    public int MaxRating { get; set; }
    
    /// <summary>
    /// List of text answers for Free Text question type
    /// </summary>
    public List<string> AnswerTexts { get; set; } = new();

    /// <summary>
    /// List of results for Multiple Options question type
    /// </summary>
    public List<QuestionOptionResultDto> OptionsResults { get; set; } = new();
}