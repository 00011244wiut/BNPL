namespace Application.Exceptions;

// Exception class representing a bad request with validation errors.
public class BadRequestException : Exception
{
    // Dictionary containing validation errors.
    public IDictionary<string, string[]> ValidationErrors { get; set; } = null!;

    // Constructor with message parameter.
    public BadRequestException(string message) : base(message)
    { }
}