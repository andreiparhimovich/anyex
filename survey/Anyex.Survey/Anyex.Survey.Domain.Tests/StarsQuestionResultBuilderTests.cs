using Anyex.Survey.Domain.Answers;
using Anyex.Survey.Domain.Questions;
using Anyex.Survey.Domain.SurveyResults;
using Anyex.Survey.Domain.SurveyResults.ResultBuilders;
using FluentAssertions;

namespace Anyex.Survey.Domain.Tests;

public class StarsQuestionResultBuilderTests
{
    [Fact]
    public void BuildResults_WhenFewAnswersSubmitted_ShouldCalculateAverageCorrectly()
    {
        // arrange
        var question = new RatingQuestion(1, "text", 10);

        var answer1 = new RatingAnswer(1, 5);
        var answer2 = new RatingAnswer(1, 5);
        var answer3 = new RatingAnswer(1, 10);
        var answer4 = new RatingAnswer(1, 10);
        var answers = new List<BaseAnswer> { answer1, answer2, answer3, answer4 };

        // act
        RatingQuestionResult result =
            (RatingQuestionResult)new StarsQuestionResultBuilder().BuildResults(question, answers);

        // assert
        result.AverageRating.Should().Be(7.5);
    }
}
