using Anyex.Survey.Domain.Answers;
using Anyex.Survey.Domain.Questions;
using Anyex.Survey.Domain.SurveyResults;
using Anyex.Survey.Domain.SurveyResults.ResultBuilders;
using FluentAssertions;

namespace Anyex.Survey.Domain.Tests;

public class MultipleChoiceQuestionResultBuilderTests
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

        var question = new MultipleChoiceQuestion(1, "text", options);

        var selectedOptions = new List<SelectedOption>
        {
            new SelectedOption(1),
            new SelectedOption(2),
            new SelectedOption(3),
            new SelectedOption(4),
        };

        var answer1 = new MultipleChoiceQuestionAnswer(1, selectedOptions);
        var answer2 = new MultipleChoiceQuestionAnswer(1, selectedOptions);
        var answer3 = new MultipleChoiceQuestionAnswer(1, selectedOptions);
        var answer4 = new MultipleChoiceQuestionAnswer(1, selectedOptions);
        var answers = new List<BaseAnswer> { answer1, answer2, answer3, answer4 };

        // act
        ChoiceQuestionResult result =
            (ChoiceQuestionResult)new MultipleChoiceQuestionResultBuilder().BuildResults(question, answers);

        // assert
        foreach (var optionsResult in result.OptionsResults)
        {
            optionsResult.Percentage.Should().Be(25);
        }
    }

    [Fact]
    public void BuildResults_WhenFourOptionsAndFourAnswers_AnswersPerPointShouldBe4()
    {
        // arrange
        var option1 = new QuestionOption(1, "text");
        var option2 = new QuestionOption(2, "text");
        var option3 = new QuestionOption(3, "text");
        var option4 = new QuestionOption(4, "text");
        var options = new List<QuestionOption> { option1, option2, option3, option4 };

        var question = new MultipleChoiceQuestion(1, "text", options);

        var selectedOptions = new List<SelectedOption>
        {
            new SelectedOption(1),
            new SelectedOption(2),
            new SelectedOption(3),
            new SelectedOption(4),
        };

        var answer1 = new MultipleChoiceQuestionAnswer(1, selectedOptions);
        var answer2 = new MultipleChoiceQuestionAnswer(1, selectedOptions);
        var answer3 = new MultipleChoiceQuestionAnswer(1, selectedOptions);
        var answer4 = new MultipleChoiceQuestionAnswer(1, selectedOptions);
        var answers = new List<BaseAnswer> { answer1, answer2, answer3, answer4 };

        // act
        ChoiceQuestionResult result =
            (ChoiceQuestionResult)new MultipleChoiceQuestionResultBuilder().BuildResults(question, answers);

        // assert
        foreach (var optionsResult in result.OptionsResults)
        {
            optionsResult.NumberOfAnswers.Should().Be(4);
        }
    }
}
