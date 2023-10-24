namespace Anyex.Survey.Domain.SurveyResults;

public class SurveyResult
{
    public string SurveyId { get; }
    public int NumberOfSubmits { get; }

    public IReadOnlyCollection<BaseQuestionResult> QuestionResults { get; }

    public SurveyResult(string surveyId, int numberOfSubmits, List<BaseQuestionResult> questionResults)
    {
        SurveyId = surveyId;

        if (numberOfSubmits < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(numberOfSubmits));
        }

        NumberOfSubmits = numberOfSubmits;

        QuestionResults = questionResults;
    }
}
