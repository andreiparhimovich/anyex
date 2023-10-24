using Anyex.Survey.Domain.Answers;
using Anyex.Survey.Domain.Questions;
using FluentAssertions;

namespace Anyex.Survey.Domain.Tests;

public class SurveyTests
{
    //[Fact]
    //public void Create_WhenNewCreated_SurveyIdShouldBePopulated()
    //{
    //    // arrange
    //    var question1 = new FreeTextQuestion(1, "test text");

    //    // act
    //    var survey = Survey.Create("title", "instructions", DateTime.UtcNow,
    //        SurveyResultsAccessType.Public, string.Empty, new List<BaseQuestion> { question1 });

    //    // assert
    //    survey.Id.Should().NotBeEmpty();
    //}

    //[Fact]
    //public void Create_WhenNewCreated_QuestionsShouldBeAssigned()
    //{
    //    // arrange
    //    var question1 = new FreeTextQuestion(1, "test text 1");
    //    var question2 = new RatingQuestion(2, "test text 2", 10);

    //    // act
    //    var survey = Survey.Create("title", "instructions", DateTime.UtcNow,
    //        SurveyResultsAccessType.Public, string.Empty, new List<BaseQuestion> { question1, question2 });

    //    // assert
    //    survey.Questions.Should().NotBeEmpty();
    //    survey.Questions.Should().HaveCount(2);
    //}

    //[Fact]
    //public void Create_WhenInconsistentStateOfIndexes_ThrowsArgumentException()
    //{
    //    // arrange
    //    var question1 = new FreeTextQuestion(1, "test text 1");
    //    var question2 = new RatingQuestion(3, "test text 2", 10);

    //    // act
    //    // assert
    //    Action act = () => Survey.Create("title", "instructions", DateTime.UtcNow,
    //        SurveyResultsAccessType.Public, string.Empty, new List<BaseQuestion> { question1, question2 });

    //    act.Should().Throw<ArgumentException>();
    //}

    //[Fact]
    //public void Restore_WhenRestored_QuestionsAndAnswersShouldBeAssigned()
    //{
    //    // arrange
    //    var question1 = new FreeTextQuestion(1, "test text 1");
    //    var question2 = new RatingQuestion(2, "test text 2", 10);
    //    var questions = new List<BaseQuestion> { question1, question2 };

    //    var answer1 = new CommentAnswer(1, "comment 1");
    //    var answer2 = new StarsAnswer(2, 10);
    //    var answers = new List<BaseAnswer> { answer1, answer2 };

    //    var surveyId = Guid.NewGuid().ToString();

    //    // act
    //    var survey = Survey.Restore(surveyId, "title", "instructions", DateTime.UtcNow,
    //        SurveyResultsAccessType.Public, string.Empty, 2, questions, answers);

    //    // assert
    //    survey.Questions.Should().HaveCount(2);
    //    survey.Answers.Should().HaveCount(2);
    //}

    //[Fact]
    //public void Submit_WhenSubmit_AnswersShouldBeAssigned()
    //{
    //    // arrange
    //    var question1 = new FreeTextQuestion(1, "test text 1");
    //    var question2 = new RatingQuestion(2, "test text 2", 10);
    //    var questions = new List<BaseQuestion> { question1, question2 };

    //    var answer1 = new CommentAnswer(1, "comment 1");
    //    var answer2 = new StarsAnswer(2, 10);
    //    var answers = new List<BaseAnswer> { answer1, answer2 };

    //    var survey = Survey.Create("title", "instructions", DateTime.UtcNow,
    //        SurveyResultsAccessType.Public, string.Empty, questions);

    //    // act
    //    survey.Submit(answers);

    //    // assert
    //    survey.Answers.Should().HaveCount(2);
    //}

    //[Fact]
    //public void Submit_WhenAnswersDontMatchQuestions_ThrowsInvalidOperationException()
    //{
    //    // arrange
    //    var question1 = new FreeTextQuestion(1, "test text 1");
    //    var question2 = new RatingQuestion(2, "test text 2", 10);
    //    var questions = new List<BaseQuestion> { question1, question2 };

    //    var answer1 = new CommentAnswer(1, "comment 1");
    //    var answer2 = new StarsAnswer(3, 10);
    //    var answers = new List<BaseAnswer> { answer1, answer2 };

    //    var survey = Survey.Create("title", "instructions", DateTime.UtcNow,
    //        SurveyResultsAccessType.Public, string.Empty, questions);

    //    // act
    //    // assert
    //    Action act = () => survey.Submit(answers);

    //    act.Should().Throw<InvalidOperationException>();
    //}

    //[Fact]
    //public void Submit_WhenSubmitTwice_NumberOfSubmitsShouldIncreaseProperly()
    //{
    //    // arrange
    //    var question1 = new FreeTextQuestion(1, "test text 1");
    //    var question2 = new RatingQuestion(2, "test text 2", 10);
    //    var questions = new List<BaseQuestion> { question1, question2 };

    //    var answer1 = new CommentAnswer(1, "comment 1");
    //    var answer2 = new StarsAnswer(2, 10);
    //    var answers = new List<BaseAnswer> { answer1, answer2 };

    //    var survey = Survey.Create("title", "instructions", DateTime.UtcNow,
    //        SurveyResultsAccessType.Public, string.Empty, questions);

    //    // act
    //    survey.Submit(answers);
    //    survey.Submit(answers);

    //    // assert
    //    survey.NumberOfSubmits.Should().Be(2);
    //}

    //[Fact]
    //public void Submit_WhenSubmitTwice_NumberOfAnswersShouldIncreaseProperly()
    //{
    //    // arrange
    //    var question1 = new FreeTextQuestion(1, "test text 1");
    //    var question2 = new RatingQuestion(2, "test text 2", 10);
    //    var questions = new List<BaseQuestion> { question1, question2 };

    //    var answer1 = new CommentAnswer(1, "comment 1");
    //    var answer2 = new StarsAnswer(2, 10);
    //    var answers = new List<BaseAnswer> { answer1, answer2 };

    //    var survey = Survey.Create("title", "instructions", DateTime.UtcNow,
    //        SurveyResultsAccessType.Public, string.Empty, questions);

    //    // act
    //    survey.Submit(answers);
    //    survey.Submit(answers);

    //    // assert
    //    survey.Answers.Should().HaveCount(4);
    //}
}