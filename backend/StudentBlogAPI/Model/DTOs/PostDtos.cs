using StudentBlogAPI.Model.Entities;

namespace StudentBlogAPI.Model.DTOs;

public record CreatePostReqDto(
    string Title,
    string Content
);

public record UpdatePostReqDto(
    string Title,
    string Content,
    PublicationStatus Status
);

public record PostResDto(
    int Id,
    int UserId,
    string Title,
    string Content,
    PublicationStatus Status,
    DateTime CreatedAt,
    DateTime UpdatedAt
);