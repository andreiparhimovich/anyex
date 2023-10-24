using Anyex.Survey.Domain.Answers;
using Anyex.Survey.Domain.Questions;

namespace Anyex.Survey.Domain.SurveyResults.ResultBuilders;

public class SingleChoiceQuestionResultBuilder : IQuestionResultBuilder
{
    public BaseQuestionResult BuildResults(BaseQuestion question, IReadOnlyCollection<BaseAnswer> answers)
    {
        var questionAnswers = answers
            .Where(x => x.QuestionIndexNumber == question.IndexNumber)
            .Select(x => (SingleChoiceQuestionAnswer)x)
            .ToList();

        var singleChoiceQuestion = (SingleChoiceQuestion)question;

        var optionResults = new List<QuestionOptionResult>();

        foreach (var questionOption in singleChoiceQuestion.Options)
        {
            var numberOfOptionAnswers = questionAnswers.Count(x =>
                x.SelectedOption.IndexNumber == questionOption.IndexNumber);

            var percentage = CalculateOptionPercentage(questionAnswers.Count, numberOfOptionAnswers);

            var optionResult =
                new QuestionOptionResult(questionOption.IndexNumber, questionOption.Text, percentage, numberOfOptionAnswers);

            optionResults.Add(optionResult);
        }

        return new ChoiceQuestionResult(question.IndexNumber, questionAnswers.Count, question.Text, optionResults,
            QuestionType.SingleOption);
    }

    private double CalculateOptionPercentage(int numberOfQuestionAnswers, int numberOfOptionAnswers)
    {
        if (numberOfOptionAnswers == 0)
        {
            return 0;
        }
        
        var value = (double)numberOfOptionAnswers / numberOfQuestionAnswers * 100;

        return Math.Round(value, 1);
    }
}