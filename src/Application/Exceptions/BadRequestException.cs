namespace Application.Exceptions;

public class BadRequestException : Exception
{
    public IDictionary<string, string[]> ValidationErrors { get; set; } = null!;
    public BadRequestException(string message) : base(message)
    { }
}
