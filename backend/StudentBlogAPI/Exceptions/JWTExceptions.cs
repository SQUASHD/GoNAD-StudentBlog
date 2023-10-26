namespace StudentBlogAPI.Exceptions;

public class InvalidJwtException : Exception
{
    private const string DefaultMessage = "Invalid JWT token.";

    public InvalidJwtException()
        : base(DefaultMessage)
    {
    }

    public InvalidJwtException(string message)
        : base(string.IsNullOrEmpty(message) ? DefaultMessage : message)
    {
    }

    public InvalidJwtException(string message, Exception inner)
        : base(string.IsNullOrEmpty(message) ? DefaultMessage : message, inner)
    {
    }
}