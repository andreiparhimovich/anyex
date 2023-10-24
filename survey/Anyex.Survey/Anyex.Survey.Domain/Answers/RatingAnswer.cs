using Anyex.Survey.Domain.Questions;

namespace Anyex.Survey.Domain.Answers;

public class RatingAnswer : BaseAnswer
{
    public int Rating { get; }

    public RatingAnswer(int questionIndexNumber, int rating)
        : base(questionIndexNumber)
    {
        if (rating <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(rating));
        }

        Rating = rating;
    }
}