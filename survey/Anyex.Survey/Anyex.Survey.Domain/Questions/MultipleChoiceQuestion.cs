namespace Anyex.Survey.Domain.Questions;

public class MultipleChoiceQuestion : BaseQuestionHasChoice
{
    public override QuestionType QuestionType => QuestionType.MultipleOption;
    public MultipleChoiceQuestion(int indexNumber, string text, List<QuestionOption> options)
        : base(indexNumber, text, options)
    {
    }
}