using System.ComponentModel.DataAnnotations;

namespace StudentBlogAPI.Model.DTOs;

public record CommentResDto(
    int Id,
    int PostId,
    int UserId,
    string Content,
    DateTime CreatedAt,
    DateTime UpdatedAt
);

public record CommentInput(
    [MinLength(6)] [MaxLength(140)] string Content
    );

public record CreateCommentDto(
    int PostId,
    int UserId,
    string Content
);

public record UpdateCommentDto(
    int Id,
    int UserId,
    string Content
);