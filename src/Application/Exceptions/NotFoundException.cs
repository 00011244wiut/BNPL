namespace Application.Exceptions;

// Exception class representing a not found error.
public class NotFoundException : Exception
{
    // Constructor with message parameter.
    public NotFoundException(string message) : base(message)
    { }
}