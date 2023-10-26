using StudentBlogAPI.Model.DTOs;

namespace StudentBlogAPI.Services.Interfaces;

public interface ICommentService
{
    Task<CommentResDto?> GetByIdAsync(int id);
    Task<CommentResDto?> CreateAsync(CreateCommentDto dto);
    Task<CommentResDto?> UpdateAsync(int currentUserId, int commentId, UpdateCommentDto dto);
    Task<CommentResDto?> DeleteAsync(int currentUserId, int commentId);
}