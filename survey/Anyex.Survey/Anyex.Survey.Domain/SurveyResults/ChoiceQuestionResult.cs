using Anyex.Survey.Domain.Questions;

namespace Anyex.Survey.Domain.SurveyResults;

public class ChoiceQuestionResult : BaseQuestionResult
{
    public override QuestionType QuestionType { get; }

    private readonly List<QuestionOptionResult> _optionsResults;

    public IReadOnlyCollection<QuestionOptionResult> OptionsResults => _optionsResults.AsReadOnly();

    public ChoiceQuestionResult(int questionIndexNumber, int numberOfAnswers, string text,
        List<QuestionOptionResult> optionsResults, QuestionType questionType) : base(questionIndexNumber, text, numberOfAnswers)
    {
        _optionsResults = optionsResults;
        QuestionType = questionType;
    }
}


/*
 * what is statistics/results?
 *  it's something what represents answers for particular survey
 *      it provides a number of properties that represent statistic/results for each type of question
 *          - for single choice question
 *          - for multiple choice question
 *          - for comment question
 *          - for stars/rating question
 *        
 *      we are going to build results based on list of answers to a particular question
 *      
 *      more interesting here is results for questions with choice:
 *          - question has options to chose
 *          - we can represent results for an entire question:
 *              totalNumberOfAnswers
 *            -we can represent results for each option:
 *              numberOfAnswers
 *              percentage
 *            
 *              how to call the type to represent this results:
 *                  these are results for an option in a question
 *                      QuestionOptionResults
 */