namespace StudentBlogAPI.Model.Internal;

public record InternalUpdateUserData(
    int UserId,
    string FirstName,
    string LastName,
    string HashedNewPassword,
    string NewSalt
);