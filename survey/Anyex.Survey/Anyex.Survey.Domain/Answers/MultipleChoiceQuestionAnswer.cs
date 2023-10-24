namespace Anyex.Survey.Domain.Answers;

public class MultipleChoiceQuestionAnswer : BaseAnswer
{
    public List<SelectedOption> SelectedOptions { get; } = new();

    public MultipleChoiceQuestionAnswer(int questionIndexNumber, List<SelectedOption> selectedOptions)
        : base(questionIndexNumber)
    {
        SelectedOptions.AddRange(selectedOptions);
    }
}