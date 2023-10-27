using System.ComponentModel.DataAnnotations;

namespace StudentBlogAPI.Model.DTOs;

public record UserResDto(
    int Id,
    string UserName,
    string FirstName,
    string LastName,
    string Email,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record UpdateUserInfoReqDto(
    string FirstName,
    string LastName
);

public record UpdatePasswordReqDto(
    [Required] string OldPassword,
    [Required] string NewPassword
);