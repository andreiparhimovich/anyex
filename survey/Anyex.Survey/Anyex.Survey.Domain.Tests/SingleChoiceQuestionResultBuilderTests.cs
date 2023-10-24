using Anyex.Survey.Domain.Answers;
using Anyex.Survey.Domain.Questions;
using Anyex.Survey.Domain.SurveyResults;
using Anyex.Survey.Domain.SurveyResults.ResultBuilders;
using FluentAssertions;

namespace Anyex.Survey.Domain.Tests;

public class SingleChoiceQuestionResultBuilderTests
{
    [Fact]
    public void BuildResults_WhenFourOptionsAndFourAnswers_PercentageShouldBe25()
    {
        // arrange
        var option1 = new QuestionOption(1, "text");
        var option2 = new QuestionOption(2, "text");
        var option3 = new QuestionOption(3, "text");
        var option4 = new QuestionOption(4, "text");
        var options = new List<QuestionOption> { option1, option2, option3, option4 };

        var question = new SingleChoiceQuestion(1, "text", options);

        var answer1 = new SingleChoiceQuestionAnswer(1, new SelectedOption(1));
        var answer2 = new SingleChoiceQuestionAnswer(1, new SelectedOption(2));
        var answer3 = new SingleChoiceQuestionAnswer(1, new SelectedOption(3));
        var answer4 = new SingleChoiceQuestionAnswer(1, new SelectedOption(4));
        var answers = new List<BaseAnswer> { answer1, answer2, answer3, answer4 };

        // act
        ChoiceQuestionResult result = (ChoiceQuestionResult) new SingleChoiceQuestionResultBuilder().BuildResults(question, answers);

        // assert
        foreach (var optionResult in result.OptionsResults)
        {
            optionResult.Percentage.Should().Be(25);
        }
    }
}
