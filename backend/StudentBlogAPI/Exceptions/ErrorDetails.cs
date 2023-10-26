using System.Text.Json;

namespace StudentBlogAPI.Exceptions;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public required string Message { get; set; }

    public override string ToString()
    {
        // This converts the ErrorDetails object into a JSON string.
        return JsonSerializer.Serialize(this);
    }
}