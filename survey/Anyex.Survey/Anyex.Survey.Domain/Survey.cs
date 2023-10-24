using Anyex.Survey.Domain.Answers;
using Anyex.Survey.Domain.Questions;
using Anyex.Survey.Domain.SurveyResults;
using Anyex.Survey.Domain.SurveyResults.ResultBuilders;

namespace Anyex.Survey.Domain;

public class Survey
{
    private const int MaxQuestionsCount = 10;

    private readonly List<BaseQuestion> _questions = new();
    private readonly List<SurveySubmit> _surveySubmits = new();

    public string Id { get; }

    public string? Title { get; private set; }
    public string? Description { get; private set; }

    public DateTime? ExpirationDateUtc { get; private set; }

    public SurveyResultAccess ResultAccess { get; private set; }

    public int NumberOfSubmits => _surveySubmits.Count;

    public bool IsExpired => ExpirationDateUtc.HasValue && DateTime.UtcNow > ExpirationDateUtc.Value;

    public IReadOnlyCollection<BaseQuestion> Questions => _questions.AsReadOnly();
    public IReadOnlyCollection<BaseAnswer> Answers => _surveySubmits.SelectMany(x => x.Answers).ToList().AsReadOnly();

    public IReadOnlyCollection<SurveySubmit> SurveySubmits => _surveySubmits.AsReadOnly();

    private Survey(string id, string? title, string? description, DateTime? expirationDateUtc,
        SurveyResultsAccessType resultAccessType, string? pinCode, List<BaseQuestion> questions, bool isNewSurvey)
    {
        Id = id;
        ResultAccess = new SurveyResultAccess(pinCode, resultAccessType);

        SetTitle(title);
        SetDescription(description);
        SetExpirationDate(expirationDateUtc, isNewSurvey);
        SetQuestions(questions);
    }

    public static Survey Create(string? title, string? description, DateTime? expirationDateUtc, 
        SurveyResultsAccessType resultAccessType, string? pinCode, List<BaseQuestion> questions)
    {
        var newSurveyId = GenerateId();

        return new Survey(newSurveyId, title, description, expirationDateUtc, resultAccessType, pinCode, questions,
            true);
    }

    public static Survey Restore(string surveyId, string? title, string? description, 
        DateTime? expirationDateUtc, SurveyResultsAccessType resultAccessType, string? pinCode, 
        List<BaseQuestion> questions, List<SurveySubmit> surveySubmits)
    {
        var survey = new Survey(surveyId, title, description, expirationDateUtc, resultAccessType, pinCode, questions,
            false);

        survey.SetSurveySubmits(surveySubmits);

        return survey;
    }

    public void Submit(List<BaseAnswer> answers)
    {
        if (IsExpired)
        {
            throw new InvalidOperationException($"Survey expired. Survey Id = {Id}");
        }

        var surveySubmit = new SurveySubmit(DateTime.UtcNow, answers);

        _surveySubmits.Add(surveySubmit);

        // TODO: generate domain events if it's needed
    }

    public SurveyResult GetResults()
    {
        return new SurveyResultBuilder().BuildResult(this);
    }

    public bool VerifyPinCode(string? pinCode)
    {
        return ResultAccess.AccessType != SurveyResultsAccessType.ByPinCode || ResultAccess.PinCode == pinCode;
    }

    private void SetTitle(string? title)
    {
        if (!string.IsNullOrEmpty(title) &&
            title.Length > 250)
        {
            throw new ArgumentOutOfRangeException(nameof(title));
        }

        Title = title;
    }

    private void SetExpirationDate(DateTime? expirationDateUtc, bool isNewSurvey)
    {
        if (expirationDateUtc.HasValue)
        {
            if (expirationDateUtc.Value.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException($"{nameof(expirationDateUtc)} must be in UTC format");
            }

            //
            // expiration date must be greater than current date time
            //
            if (isNewSurvey && expirationDateUtc.Value < DateTime.UtcNow)
            {
                throw new ArgumentException($"{nameof(expirationDateUtc)} must be greater than current date time");
            }
        }

        ExpirationDateUtc = expirationDateUtc;
    }

    private void SetDescription(string? description)
    {
        if (!string.IsNullOrEmpty(description) &&
            description.Length > 1000)
        {
            throw new ArgumentOutOfRangeException(nameof(description));
        }

        Description = description;
    }

    private void SetQuestions(List<BaseQuestion> questions)
    {
        //
        // check questions max count
        //
        if (questions.Count > MaxQuestionsCount)
        {
            throw new ArgumentException(
                $"Number of questions exceed the maximum value. Maximum is {MaxQuestionsCount}. Actual is {questions.Count}");
        }
        
        //
        //check if question indexes have correct order
        //
        var indexes = questions.Select(x => x.IndexNumber).ToList();

        for (int index = 1; index <= questions.Count; index++)
        {
            if (!indexes.Contains(index))
            {
                throw new ArgumentException("Inconsistent state of the question's indexes");
            }
        }

        _questions.AddRange(questions);
    }

    private void SetSurveySubmits(List<SurveySubmit> surveySubmits)
    {
        _surveySubmits.AddRange(surveySubmits);
    }

    private static string GenerateId()
    {
        var guid = Guid.NewGuid();
        return Convert.ToBase64String(guid.ToByteArray())
            .Replace("/", "-")
            .Replace("+", "_")
            .Replace("=", "");
    }
}