namespace StudentBlogAPI.Model.Internal;

public record InternalCreatePostData(
    int CurrentUserId,
    string Title,
    string Content
);

public record InternalUpdatePostData(
    int CurrenUserId,
    int PostId,
    string Title,
    string Content
);

public record InternalDeletePostData(
    int PostId,
    int UserId
);