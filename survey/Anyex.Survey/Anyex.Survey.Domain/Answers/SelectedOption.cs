namespace Anyex.Survey.Domain.Answers;

public class SelectedOption
{
    public int IndexNumber { get; }

    public SelectedOption(int indexNumber)
    {
        if (indexNumber <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(indexNumber));
        }

        IndexNumber = indexNumber;
    }
}