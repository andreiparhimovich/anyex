using Anyex.Survey.Domain.Answers;

namespace Anyex.Survey.Domain;

public class SurveySubmit
{
    public DateTime SubmitDateUtc { get; }
    public IEnumerable<BaseAnswer> Answers { get; }

    public SurveySubmit(DateTime submitDateUtc, IEnumerable<BaseAnswer> answers)
    {
        SubmitDateUtc = submitDateUtc;
        Answers = answers;
    }
}