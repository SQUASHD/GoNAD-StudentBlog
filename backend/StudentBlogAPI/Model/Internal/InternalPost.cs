namespace StudentBlogAPI.Model.Internal;

public record InternalUpdatePostData(
    int UserId,
    string Title,
    string Content
);

public record InternalCreatePostData(
    int UserId,
    string Title,
    string Content
);