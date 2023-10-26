namespace StudentBlogAPI.Model.DTOs;

public record PostInput(
    string Title,
    string Content
);

public record CreatePostDto(
    int UserId,
    string Title,
    string Content
);

public record UpdatePostDto(
    int UserId,
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