using Anyex.Survey.Domain.Questions;

namespace Anyex.Survey.Domain.SurveyResults.ResultBuilders;

public class SurveyResultBuilder
{
    public SurveyResult BuildResult(Survey survey)
    {
        var questionResults = new List<BaseQuestionResult>();

        foreach (var question in survey.Questions)
        {
            BaseQuestionResult questionResult;

            switch (question.QuestionType)
            {
                case QuestionType.SingleOption:
                    questionResult =
                        new SingleChoiceQuestionResultBuilder().BuildResults(question, survey.Answers);
                    break;
                case QuestionType.MultipleOption:
                    questionResult =
                        new MultipleChoiceQuestionResultBuilder().BuildResults(question, survey.Answers);
                    break;
                case QuestionType.FreeText:
                    questionResult =
                        new FreeTextQuestionResultBuilder().BuildResults(question, survey.Answers);
                    break;
                case QuestionType.Rating:
                    questionResult =
                        new StarsQuestionResultBuilder().BuildResults(question, survey.Answers);
                    break;
                default:
                    throw new Exception($"Question Type {question.QuestionType} is not allowed");
            }

            questionResults.Add(questionResult);
        }

        return new SurveyResult(survey.Id, survey.NumberOfSubmits, questionResults);
    }
}
