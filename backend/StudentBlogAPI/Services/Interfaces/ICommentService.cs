using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Internal;
using StudentBlogAPI.Utilities;

namespace StudentBlogAPI.Services.Interfaces;

public interface ICommentService
{
    Task<PaginatedResultDto<CommentResDto>> GetAllAsync(int pageNumber, int pageSize);
    Task<ICollection<CommentResDto>> GetCommentsByPostIdAsync(int postId);
    Task<CommentResDto?> CreateAsync(InternalCreateCommentData data);
    Task<CommentResDto?> UpdateAsync(InternalUpdateCommentData data);
    Task<CommentResDto?> DeleteAsync(InternalDeleteCommentData data);
}