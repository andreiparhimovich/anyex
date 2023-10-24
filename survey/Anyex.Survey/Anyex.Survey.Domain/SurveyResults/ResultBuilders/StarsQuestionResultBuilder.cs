using Anyex.Survey.Domain.Answers;
using Anyex.Survey.Domain.Questions;

namespace Anyex.Survey.Domain.SurveyResults.ResultBuilders;

public class StarsQuestionResultBuilder : IQuestionResultBuilder
{
    public BaseQuestionResult BuildResults(BaseQuestion question, IReadOnlyCollection<BaseAnswer> answers)
    {
        var questionAnswers = answers
            .Where(x => x.QuestionIndexNumber == question.IndexNumber)
            .Select(x => (RatingAnswer)x)
            .ToList();

        double average = 0;
        
        //
        // we need to check if the collection has any elements because
        // Average() throws an exception if there are no elements
        //
        if (questionAnswers.Any())
        {
            average = Math.Round(questionAnswers.Average(x => x.Rating), 1);
        }

        //
        // get max rating value from question
        //
        var maxValue = ((RatingQuestion)question).MaxRating;

        return new RatingQuestionResult(question.IndexNumber, question.Text, questionAnswers.Count, average, maxValue);
    }
}