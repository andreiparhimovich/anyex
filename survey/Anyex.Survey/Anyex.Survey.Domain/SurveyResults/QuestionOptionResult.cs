namespace Anyex.Survey.Domain.SurveyResults;

public class QuestionOptionResult
{
    public int OptionIndexNumber { get; }
    public string OptionText { get; }
    public double Percentage { get; }
    public int NumberOfAnswers { get; }

    public QuestionOptionResult(int optionIndexNumber, string optionText, double percentage, int numberOfAnswers)
    {
        if (optionIndexNumber <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(optionIndexNumber));
        }

        OptionIndexNumber = optionIndexNumber;

        if (string.IsNullOrWhiteSpace(optionText))
        {
            throw new ArgumentNullException(nameof(optionText));
        }

        OptionText = optionText;

        if (numberOfAnswers < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numberOfAnswers));
        }

        NumberOfAnswers = numberOfAnswers;

        if (percentage < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(percentage));
        }

        Percentage = percentage;
    }
}
