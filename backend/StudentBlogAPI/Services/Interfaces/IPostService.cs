using StudentBlogAPI.Model.DTOs;

namespace StudentBlogAPI.Services.Interfaces;

public interface IPostService
{
    Task<ICollection<PostResDto>> GetAllAsync();
    Task<PostResDto?> GetByIdAsync(int id);
    Task<PostResDto?> CreateAsync(int userId, CreatePostDto dto);
    Task<PostResDto?> UpdateAsync(int userId, int postId, UpdatePostDto dto);
    Task<PostResDto?> DeleteAsync(int userId, int postId);
}