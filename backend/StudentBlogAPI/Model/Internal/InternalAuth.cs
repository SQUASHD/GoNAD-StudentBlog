namespace StudentBlogAPI.Model.Internal;

public record InternalAuthData(
    string UserName,
    string FirstName,
    string LastName,
    string Email,
    string HashedPassword,
    string Salt
);

public record InternalRevokeTokenData(
    string Token,
    int UserId
);