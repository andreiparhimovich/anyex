namespace Anyex.Survey.Domain.Questions;

public class QuestionOption
{
    public int IndexNumber { get; }
    public string Text { get; }

    public QuestionOption(int indexNumber, string text)
    {
        if (indexNumber <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(indexNumber));
        }

        IndexNumber = indexNumber;

        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (text.Length > 250)
        {
            throw new ArgumentException(nameof(text));
        }

        Text = text;
    }
}