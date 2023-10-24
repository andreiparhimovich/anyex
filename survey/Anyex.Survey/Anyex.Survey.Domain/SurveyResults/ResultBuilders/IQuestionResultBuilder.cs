using Anyex.Survey.Domain.Answers;
using Anyex.Survey.Domain.Questions;

namespace Anyex.Survey.Domain.SurveyResults.ResultBuilders;

public interface IQuestionResultBuilder
{
    BaseQuestionResult BuildResults(BaseQuestion question, IReadOnlyCollection<BaseAnswer> answers);
}