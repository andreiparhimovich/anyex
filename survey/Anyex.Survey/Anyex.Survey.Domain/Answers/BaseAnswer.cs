namespace Anyex.Survey.Domain.Answers;

public abstract class BaseAnswer
{
    public int QuestionIndexNumber { get; }

    protected BaseAnswer(int questionIndexNumber)
    {
        if (questionIndexNumber <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(questionIndexNumber));
        }

        QuestionIndexNumber = questionIndexNumber;
    }
}
