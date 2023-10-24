using Anyex.Survey.Domain.Questions;

namespace Anyex.Survey.Domain.SurveyResults;

public class FreeTextQuestionResult : BaseQuestionResult
{
    public override QuestionType QuestionType => QuestionType.FreeText;
    public List<string> AnswerTexts { get; }

    public FreeTextQuestionResult(int questionIndexNumber, string text, int numberOfAnswers, List<string> answerTexts) 
        : base(questionIndexNumber, text, numberOfAnswers)
    {
        AnswerTexts = answerTexts;
    }
}
