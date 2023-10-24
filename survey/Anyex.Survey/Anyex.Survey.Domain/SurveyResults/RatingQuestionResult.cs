using Anyex.Survey.Domain.Questions;

namespace Anyex.Survey.Domain.SurveyResults;

public class RatingQuestionResult : BaseQuestionResult
{
    public override QuestionType QuestionType => QuestionType.Rating;
    public double AverageRating { get;}
    public int MaxRating { get; }

    public RatingQuestionResult(int questionIndexNumber, string text, int numberOfAnswers, double averageRating, int maxRating) 
        : base(questionIndexNumber, text, numberOfAnswers)
    {
        if (averageRating < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(averageRating));
        }

        AverageRating = averageRating;

        MaxRating = maxRating;
    }
}
