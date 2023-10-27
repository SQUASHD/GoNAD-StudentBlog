using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Services.Interfaces;

public interface IPostService
{
    Task<PaginatedResultDto<PostResDto>> GetAllAsync(int pageNumber, int pageSize);
    Task<PostResDto?> GetByIdAsync(int id);
    Task<PostResDto?> CreateAsync(InternalCreatePostData data);
    Task<PostResDto?> UpdateAsync(InternalUpdatePostData data);
    Task<PostResDto?> DeleteAsync(InternalDeletePostData data);
}