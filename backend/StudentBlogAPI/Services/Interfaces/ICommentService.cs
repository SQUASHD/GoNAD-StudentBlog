using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Services.Interfaces;

public interface ICommentService
{
    Task<CommentResDto?> GetByIdAsync(int id);
    Task<CommentResDto?> CreateAsync(InternalCreateCommentData data);
    Task<CommentResDto?> UpdateAsync(InternalUpdateCommentData data);
    Task<CommentResDto?> DeleteAsync(InternalDeleteCommentData data);
}