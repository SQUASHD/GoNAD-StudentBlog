using StudentBlogAPI.Model.Entities;

namespace StudentBlogAPI.Repository.Interfaces;

public interface IPostRepository
{
    Task<(ICollection<Post>, int)> GetAllAsync(int pageNumber, int pageSize);
    Task<ICollection<Post>> GetPostsByUserIdAsync(int userId);
    Task<Post?> GetByIdAsync(int id);
    Task<Post> CreateAsync(Post entity);
    Task<Post> UpdateAsync(Post entity);
    Task<Post> DeleteAsync(Post entity);
}