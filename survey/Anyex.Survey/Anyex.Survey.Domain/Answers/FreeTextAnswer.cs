namespace Anyex.Survey.Domain.Answers;

public class FreeTextAnswer : BaseAnswer
{
    public string Text { get; set; }

    public FreeTextAnswer(int questionIndexNumber, string text)
        : base(questionIndexNumber)
    {
        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        Text = text;
    }
}