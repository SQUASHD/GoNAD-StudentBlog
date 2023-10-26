namespace StudentBlogAPI.Model.Internal;

public record InternalCreatePostData(
    int UserId,
    string Title,
    string Content
);