namespace Anyex.Survey.Domain.Questions;

public class FreeTextQuestion : BaseQuestion
{
    public override QuestionType QuestionType => QuestionType.FreeText;

    public FreeTextQuestion(int indexNumber, string text) : base(indexNumber, text)
    {
    }
}