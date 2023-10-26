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

public record UpdateUserInputReqDto(
    string FirstName,
    string LastName,
    string OldPassword,
    string NewPassword
);

