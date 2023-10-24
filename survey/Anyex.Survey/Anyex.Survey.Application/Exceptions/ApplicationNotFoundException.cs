namespace Anyex.Survey.Application.Exceptions;

public class ApplicationNotFoundException : Exception
{
    public ApplicationNotFoundException(string message) : base(message) { }

    public ApplicationNotFoundException(string message, Exception exception) : base(message, exception) { }
}