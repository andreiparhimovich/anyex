namespace Anyex.Survey.Domain.Questions;

public abstract class BaseQuestion
{
    public int IndexNumber { get; }
    public string Text { get; }

    public abstract QuestionType QuestionType { get; }

    protected BaseQuestion(int indexNumber, string text)
    {
        if (indexNumber <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(indexNumber));
        }

        IndexNumber = indexNumber;

        if (string.IsNullOrEmpty(text))
        {
            throw new ArgumentNullException(nameof(text));
        }

        if (text.Length > 250)
        {
            throw new ArgumentException(nameof(text));
        }

        Text = text;
    }
}

/*
 * What if we have question with answers how the logic should be changed?
 *
 *  Needs to resolve how to deal with question because question becomes not really simple object but has own logic
 *
 *  so we have 2 objects with logic
 *      survey
 *      question
 *
 *      survey has a list of questions
 *          but they cant be stored separately
 *
 *      what if there should be difference between survey as list of question aka readonly
 *      and survey as an object that contains logic
 *
 *      now defacto survey has questions as readonly collection ans store answers separately (probably not really bad idea)
 *
 */