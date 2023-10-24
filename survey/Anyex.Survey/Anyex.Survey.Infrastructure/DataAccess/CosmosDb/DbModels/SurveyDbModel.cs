using Anyex.Survey.Domain;
using Anyex.Survey.Domain.Questions;
using Newtonsoft.Json;

namespace Anyex.Survey.Infrastructure.DataAccess.CosmosDb.DbModels
{
    public record SurveyDbModel(
        [JsonProperty(PropertyName = "id")]
        string Id,
        string? Title,
        string? Description,
        DateTime? ExpirationDateUtc,
        SurveyResultsAccessType ResultAccessType,
        string? PinCode,
        List<QuestionDbModel> Questions,
        List<SurveySubmitDbModel> Submits);

    public record QuestionDbModel(
        int IndexNumber,
        string? Text,
        QuestionType QuestionType,
        int MaxRating,
        List<QuestionOptionDbModel> Options
    );

    public record QuestionOptionDbModel(
        int IndexNumber,
        string? Text
    );

    public record SurveySubmitDbModel(
        DateTime SubmitDateUtc,
        List<AnswerDbModel> Answers
    );

    public record AnswerDbModel(
        int QuestionIndexNumber,
        QuestionType QuestionType,
        SelectedOptionDbModel? SelectedOption,
        List<SelectedOptionDbModel> SelectedOptions,
        int Rating,
        string? Text
    );

    public record SelectedOptionDbModel(int IndexNumber);

}
