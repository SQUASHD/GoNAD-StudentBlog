using StudentBlogAPI.Model.Entities;

namespace StudentBlogAPI.Repository.Interfaces;

public interface ICommentRepository
{
    Task<(ICollection<Comment>, int)> GetAllAsync(int pageNumber, int pageSize);
    Task<ICollection<Comment>> GetCommentsByPostIdAsync(int postId);
    Task<Comment?> GetByIdAsync(int id);
    Task<Comment> CreateAsync(Comment entity);
    Task<Comment> UpdateAsync(Comment entity);
    Task<Comment> DeleteAsync(Comment entity);
}