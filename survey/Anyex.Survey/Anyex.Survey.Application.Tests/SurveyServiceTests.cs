using Anyex.Survey.Application.DataAccess;
using Anyex.Survey.Application.Exceptions;
using Anyex.Survey.Application.Services.Survey;
using Anyex.Survey.Domain;
using Anyex.Survey.Domain.Questions;
using FluentAssertions;
using Moq;

namespace Anyex.Survey.Application.Tests
{
    public class SurveyServiceTests
    {
    //    [Fact]
    //    public async Task GetSurveyByIdAsync_WhenRepositoryReturnsSurvey_ShouldReturnSurvey()
    //    {
    //        // arrange
    //        var question1 = new FreeTextQuestion(1, "test text");

    //        var survey = Domain.Survey.Create("title", "instructions", DateTime.UtcNow,
    //        SurveyResultsAccessType.Public, string.Empty, new List<BaseQuestion> { question1 });

    //        var surveyRepositoryMock = new Mock<IRepository<Domain.Survey, Guid>>();
    //        surveyRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync(survey);

    //        var surveyService = new SurveyService(surveyRepositoryMock.Object);

    //        // act
    //        var result = await surveyService.GetSurveyByIdAsync(survey.Id);

    //        // assert
    //        result.Should().NotBeNull();
    //    }

    //    [Fact]
    //    public async Task GetSurveyByIdAsync_WhenRepositoryReturnsNull_ShouldThrowApplicationNotFoundException()
    //    {
    //        // arrange
    //        var surveyRepositoryMock = new Mock<IRepository<Domain.Survey, Guid>>();
    //        surveyRepositoryMock.Setup(m => m.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Domain.Survey?)null);

    //        var surveyService = new SurveyService(surveyRepositoryMock.Object);

    //        // act
    //        // assert
    //        Func<Task> act = () => surveyService.GetSurveyByIdAsync(Guid.NewGuid());
    //        await act.Should().ThrowAsync<ApplicationNotFoundException>();
    //    }

    //    [Fact]
    //    public async Task CreateSurveyAsync_WhenEverythingIsValid_ShouldReturnSurvey()
    //    {
    //        // arrange
    //        var surveyRepositoryMock = new Mock<IRepository<Domain.Survey, Guid>>();
    //        surveyRepositoryMock
    //            .Setup(m => m.CreateAsync(It.IsAny<Domain.Survey>()))
    //            .ReturnsAsync((Domain.Survey survey) => survey);

    //        var surveyService = new SurveyService(surveyRepositoryMock.Object);

    //        var questionDto1 = new QuestionDto
    //            { IndexNumber = 1, QuestionType = QuestionType.FreeText, Text = "test text 1" };

    //        var questionDto2 = new QuestionDto
    //            { IndexNumber = 2, QuestionType = QuestionType.FreeText, Text = "test text 2" };

    //        var questions = new List<QuestionDto> { questionDto1, questionDto2 };

    //        // act
    //        var result = await surveyService.CreateSurveyAsync(questions);

    //        // assert
    //        result.Should().NotBeNull();
    //        result.Questions.Should().HaveCount(2);
    //    }

    //    [Fact]
    //    public async Task CreateSurveyAsync_WhenInvalidModel_ShouldReturnApplicationValidationException()
    //    {
    //        // arrange
    //        var surveyRepositoryMock = new Mock<IRepository<Domain.Survey, Guid>>();
    //        surveyRepositoryMock
    //            .Setup(m => m.CreateAsync(It.IsAny<Domain.Survey>()))
    //            .ReturnsAsync((Domain.Survey survey) => survey);

    //        var surveyService = new SurveyService(surveyRepositoryMock.Object);

    //        var questionDto1 = new QuestionDto
    //            { IndexNumber = -1, QuestionType = QuestionType.FreeText, Text = "test text 1" };

    //        var questionDto2 = new QuestionDto
    //            { IndexNumber = 2, QuestionType = QuestionType.FreeText, Text = "test text 2" };

    //        var questions = new List<QuestionDto> { questionDto1, questionDto2 };

    //        // act
    //        // assert
    //        Func<Task> act = () => surveyService.CreateSurveyAsync(questions);
    //        await act.Should().ThrowAsync<ApplicationValidationException>();
    //    }

    //    [Fact]
    //    public async Task SubmitAnswersAsync_WhenModelIsValid_ShouldSubmitAnswers()
    //    {
    //        // arrange
    //        var question = new FreeTextQuestion(1, "test text");

    //        var survey = Domain.Survey.Create("title", "instructions", DateTime.UtcNow,
    //        SurveyResultsAccessType.Public, string.Empty, new List<BaseQuestion> { question });

    //        var surveyRepositoryMock = new Mock<IRepository<Domain.Survey, Guid>>();
    //        surveyRepositoryMock
    //            .Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
    //            .ReturnsAsync(survey);
    //        surveyRepositoryMock.Setup(m => m.UpdateAsync(It.IsAny<Domain.Survey>()));

    //        var answerDto = new AnswerDto
    //            { QuestionIndexNumber = 1, QuestionType = QuestionType.FreeText, FreeText = "comment" };

    //        var answers = new List<AnswerDto> { answerDto };

    //        var surveyService = new SurveyService(surveyRepositoryMock.Object);

    //        // act
    //        await surveyService.SubmitAnswersAsync(survey.Id, answers);

    //        // assert
    //        surveyRepositoryMock
    //            .Verify(x => x.UpdateAsync(It.IsAny<Domain.Survey>()), Times.Exactly(1));
    //    }

    //    [Fact]
    //    public async Task SubmitAnswersAsync_WhenInvalidModel_ShouldThrowApplicationValidationException()
    //    {
    //        // arrange
    //        var question = new FreeTextQuestion(1, "test text");

    //        var survey = Domain.Survey.Create("title", "instructions", DateTime.UtcNow,
    //        SurveyResultsAccessType.Public, string.Empty, new List<BaseQuestion> { question });

    //        var surveyRepositoryMock = new Mock<IRepository<Domain.Survey, Guid>>();
    //        surveyRepositoryMock
    //            .Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
    //            .ReturnsAsync(survey);

    //        var answerDto = new AnswerDto
    //            { QuestionIndexNumber = -1, QuestionType = QuestionType.FreeText, FreeText = "comment" };

    //        var answers = new List<AnswerDto> { answerDto };

    //        var surveyService = new SurveyService(surveyRepositoryMock.Object);

    //        // act
    //        // assert
    //        Func<Task> act = () => surveyService.SubmitAnswersAsync(survey.Id, answers);
    //        await act.Should().ThrowAsync<ApplicationValidationException>();
    //    }

    //    [Fact]
    //    public async Task SubmitAnswersAsync_WhenThereIsNoSurveyInDatabse_ShouldThrowApplicationNotFoundException()
    //    {
    //        // arrange
    //        var surveyRepositoryMock = new Mock<IRepository<Domain.Survey, Guid>>();
    //        surveyRepositoryMock
    //            .Setup(m => m.GetByIdAsync(It.IsAny<Guid>()))
    //            .ReturnsAsync((Domain.Survey?)null);

    //        var surveyService = new SurveyService(surveyRepositoryMock.Object);

    //        var answerDto = new AnswerDto
    //            { QuestionIndexNumber = -1, QuestionType = QuestionType.FreeText, FreeText = "comment" };

    //        var answers = new List<AnswerDto> { answerDto };

    //        // act
    //        // assert
    //        Func<Task> act = () => surveyService.SubmitAnswersAsync(Guid.NewGuid(), answers);
    //        await act.Should().ThrowAsync<ApplicationNotFoundException>();
    //    }
    }
}