namespace StudentBlogAPI.Validators;

public class ValidationErrorResponse
{
    public string Title { get; set; } = "Validation Error";
    public int Status { get; set; } = 400;
    public List<ValidationError> Errors { get; set; } = new();
}

public class ValidationError
{
    public required string Field { get; set; }
    public required string Message { get; set; }
}