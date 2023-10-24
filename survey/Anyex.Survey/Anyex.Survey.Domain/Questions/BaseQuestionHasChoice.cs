namespace Anyex.Survey.Domain.Questions;

public abstract class BaseQuestionHasChoice : BaseQuestion
{
    private readonly List<QuestionOption> _options;

    public IReadOnlyCollection<QuestionOption> Options => _options.AsReadOnly();

    protected BaseQuestionHasChoice(int indexNumber, string text, List<QuestionOption> options)
        : base(indexNumber, text)
    {
        _options = new List<QuestionOption>(options);
    }
}