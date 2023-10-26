namespace StudentBlogAPI.Model.DTOs;

public record PostInputReqDto(
    string Title,
    string Content
);

public record PostResDto(
    int Id,
    int UserId,
    string Title,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt
);