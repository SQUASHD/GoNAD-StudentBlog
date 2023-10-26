using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using StudentBlogAPI.Data;

namespace StudentBlogAPI.Validation.UserValidator;

public class UniqueUsernameAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var dbContext = (StudentBlogDbContext)validationContext.GetService(typeof(StudentBlogDbContext))!;
        var entity = dbContext.Users.AsNoTracking().FirstOrDefault(u => value != null && u.UserName == (string)value);

        return entity != null ? new ValidationResult(GetErrorMessage(value?.ToString())) : ValidationResult.Success;
    }

    private string GetErrorMessage(string? username)
    {
        return $"Username '{username}' is already taken.";
    }
}