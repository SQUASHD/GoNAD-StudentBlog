namespace StudentBlogAPI.Model.Internal;

public record InternalCreateCommentData(
    int PostId,
    int UserId,
    string Content
);

public record InternalUpdateCommentData(
    int Id,
    int UserId,
    string Content
);