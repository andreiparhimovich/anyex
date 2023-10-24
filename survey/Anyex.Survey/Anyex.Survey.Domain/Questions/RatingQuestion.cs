namespace Anyex.Survey.Domain.Questions;

public class RatingQuestion : BaseQuestion
{
    public override QuestionType QuestionType => QuestionType.Rating;
    public int MaxRating { get; }

    public RatingQuestion(int indexNumber, string text, int maxRating) : base(indexNumber, text)
    {
        if (maxRating <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxRating));
        }

        MaxRating = maxRating;
    }
}