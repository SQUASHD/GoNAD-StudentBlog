using System.ComponentModel.DataAnnotations;
using StudentBlogAPI.Validation.UserValidator;

namespace StudentBlogAPI.Model.DTOs;

public record AuthResDto(
    int Id,
    string UserName
);

public record AuthResWithTokenDto(
    int Id,
    string UserName,
    string AccessToken,
    string RefreshToken
);

public record UserRegisterDto(
    [MinLength(3)] [MaxLength(20)] string UserName,
    string FirstName,
    string LastName,
    [ValidEmail] string Email,
    string Password
);

public record UserLoginDto(
    string UserName,
    string Password
);