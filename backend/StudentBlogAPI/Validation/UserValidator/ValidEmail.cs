using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace StudentBlogAPI.Validation.UserValidator;

public partial class ValidEmail : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var email = (string)value!;
        var emailRegex = MyRegex();

        return !emailRegex.IsMatch(email) ? new ValidationResult(GetErrorMessage(email)) : ValidationResult.Success;
    }

    private string GetErrorMessage(string? email)
    {
        return $"Email '{email}' is not valid.";
    }

    [GeneratedRegex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$")]
    private static partial Regex MyRegex();
}