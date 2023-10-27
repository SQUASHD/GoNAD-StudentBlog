namespace StudentBlogAPI.Model.Internal;

public record InternalAuthResData(
    int Id,
    string UserName
);

public record InternalProcessedAuthData(
    string UserName,
    string FirstName,
    string LastName,
    string Email,
    string HashedPassword,
    string Salt
);