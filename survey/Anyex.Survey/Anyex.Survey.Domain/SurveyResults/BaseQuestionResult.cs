using Anyex.Survey.Domain.Questions;

namespace Anyex.Survey.Domain.SurveyResults;

public abstract class BaseQuestionResult
{
    public int QuestionIndexNumber { get; }
    public abstract QuestionType QuestionType { get; }
    public string Text { get; }
    public int NumberOfAnswers { get; }

    protected BaseQuestionResult(int questionIndexNumber, string text, int numberOfAnswers)
    {
        if (questionIndexNumber <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(questionIndexNumber));
        }

        QuestionIndexNumber = questionIndexNumber;

        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        Text = text;

        if (numberOfAnswers < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numberOfAnswers));
        }

        NumberOfAnswers = numberOfAnswers;
    }
}
