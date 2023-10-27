namespace StudentBlogAPI.Model.Internal;

public record InternalRevokeTokenData(
    string Token,
    int UserId
);