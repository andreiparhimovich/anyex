namespace Anyex.Survey.Application.Exceptions;

public class ApplicationValidationException : Exception
{
    public ApplicationValidationException(string message) : base(message) { }

    public ApplicationValidationException(string message, Exception exception) : base(message, exception) { }
}