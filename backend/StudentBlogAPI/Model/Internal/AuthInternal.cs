namespace StudentBlogAPI.Model.Internal;

public record AuthInternalDto(
    string UserName,
    string FirstName,
    string LastName,
    string Email,
    string HashedPassword,
    string Salt
);

public record RevokeDataInternalDto(
    string Token,
    int UserId
    );