using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Services.Interfaces;

public interface IPostService
{
    Task<ICollection<PostResDto>> GetAllAsync();
    Task<PostResDto?> GetByIdAsync(int id);
    Task<PostResDto?> CreateAsync(int userId, InternalCreatePostData data);
    Task<PostResDto?> UpdateAsync(int userId, int postId, InternalUpdatePostData data);
    Task<PostResDto?> DeleteAsync(int userId, int postId);
}