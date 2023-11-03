using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Repository.Interfaces;

public interface IPostRepository
{
    Task<(ICollection<Post>, int)> GetAllAsync(InternalGetAllPostsData data);
    Task<(ICollection<Post>, int)> GetPostsByUserIdAsync(int userId, int pageNumber, int pageSize);
    Task<Post?> GetByIdAsync(int id);
    Task<Post> CreateAsync(Post entity);
    Task<Post> UpdateAsync(Post entity);
    Task<Post> DeleteAsync(Post entity);
}