using StudentBlogAPI.Model.Entities;

namespace StudentBlogAPI.Model.Internal;

public record InternalGetAllPostsData(
    int PageNumber,
    int PageSize,
    string OrderBy
);

public record InternalCreatePostData(
    int CurrentUserId,
    string Title,
    string Content
);

public record InternalGetPostByIdData(
    int CurrentUserId,
    int PostId
);

public record InternalUpdatePostData(
    int CurrentUserId,
    int PostId,
    string Title,
    string Content,
    PublicationStatus Status
);

public record InternalDeletePostData(
    int PostId,
    int UserId
);