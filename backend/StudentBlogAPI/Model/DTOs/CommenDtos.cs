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

public record CommentInputReqDto(
    [MinLength(6)] [MaxLength(140)] string Content
    );