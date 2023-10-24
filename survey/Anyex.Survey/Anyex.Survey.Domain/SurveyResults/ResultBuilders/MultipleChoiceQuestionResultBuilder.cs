using Anyex.Survey.Domain.Answers;
using Anyex.Survey.Domain.Questions;

namespace Anyex.Survey.Domain.SurveyResults.ResultBuilders;

public class MultipleChoiceQuestionResultBuilder : IQuestionResultBuilder
{
    public BaseQuestionResult BuildResults(BaseQuestion question, IReadOnlyCollection<BaseAnswer> answers)
    {
        var questionAnswers = answers
            .Where(x => x.QuestionIndexNumber == question.IndexNumber)
            .Select(x => (MultipleChoiceQuestionAnswer)x)
            .ToList();

        var numberOfSelections = questionAnswers.Sum(x => x.SelectedOptions.Count);

        var multipleChoiceQuestion = (MultipleChoiceQuestion)question;

        var optionResults = new List<QuestionOptionResult>();

        foreach (var questionOption in multipleChoiceQuestion.Options)
        {
            var numberOfOptionAnswers = questionAnswers.Count(answer =>
                answer.SelectedOptions.Any(answerOption =>
                    answerOption.IndexNumber == questionOption.IndexNumber));

            var percentage = CalculateOptionPercentage(numberOfSelections, numberOfOptionAnswers);

            var optionResult =
                new QuestionOptionResult(questionOption.IndexNumber, questionOption.Text, percentage, numberOfOptionAnswers);

            optionResults.Add(optionResult);
        }

        return new ChoiceQuestionResult(question.IndexNumber, questionAnswers.Count, question.Text, optionResults, QuestionType.MultipleOption);
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
