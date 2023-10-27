using System.ComponentModel.DataAnnotations;
using StudentBlogAPI.Validation.UserValidator;

namespace StudentBlogAPI.Model.DTOs;

public record AuthWithTokenResDto(
    int Id,
    string UserName,
    string AccessToken,
    string RefreshToken
);

public record UserRegisterReqDto(
    [MinLength(3)] [MaxLength(20)] string UserName,
    string FirstName,
    string LastName,
    [ValidEmail] string Email,
    string Password
);

public record UserLoginReqDto(
    string UserName,
    string Password
);