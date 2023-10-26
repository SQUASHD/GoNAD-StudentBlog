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

public record UpdateUserInput(
    string FirstName,
    string LastName,
    string OldPassword,
    string NewPassword
);

public record UpdateUserData(
    int UserId,
    string FirstName,
    string LastName,
    string HashedNewPassword,
    string NewSalt
);