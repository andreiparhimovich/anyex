namespace Anyex.Survey.Domain.Answers;

public class SingleChoiceQuestionAnswer : BaseAnswer
{
    public SelectedOption SelectedOption { get; }

    public SingleChoiceQuestionAnswer(int questionIndexNumber, SelectedOption selectedOption)
        : base(questionIndexNumber)
    {
        SelectedOption = selectedOption;
    }
}