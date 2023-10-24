namespace Anyex.Survey.Domain.Questions;

public class SingleChoiceQuestion : BaseQuestionHasChoice
{
    public override QuestionType QuestionType => QuestionType.SingleOption;

    public SingleChoiceQuestion(int indexNumber, string text, List<QuestionOption> options)
        : base(indexNumber, text, options)
    {
    }
}