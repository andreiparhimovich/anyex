using Anyex.Survey.Domain.Answers;
using Anyex.Survey.Domain.Questions;

namespace Anyex.Survey.Domain.SurveyResults.ResultBuilders;

public class FreeTextQuestionResultBuilder : IQuestionResultBuilder
{
    public BaseQuestionResult BuildResults(BaseQuestion question, IReadOnlyCollection<BaseAnswer> answers)
    {
        var questionAnswers = answers
            .Where(x => x.QuestionIndexNumber == question.IndexNumber)
            .Select(x => (FreeTextAnswer)x)
            .ToList();

        var listOfAnswerTexts = questionAnswers.Select(x => x.Text).ToList();

        return new FreeTextQuestionResult(question.IndexNumber, question.Text, questionAnswers.Count, listOfAnswerTexts);
    }
}