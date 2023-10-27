namespace StudentBlogAPI.Model.Internal;

public record InternalCreateCommentData(
    int CurrentUserId,
    int PostId,
    string Content
);

public record InternalUpdateCommentData(
    int CurrentUserId,
    int CommentId,
    string Content
);

public record InternalDeleteCommentData(
    int CurrentUserId,
    int CommentId
);