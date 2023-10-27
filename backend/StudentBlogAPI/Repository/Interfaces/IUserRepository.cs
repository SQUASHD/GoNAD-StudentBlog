using StudentBlogAPI.Model.Entities;

namespace StudentBlogAPI.Repository.Interfaces;

public interface IUserRepository
{
    Task<(ICollection<User>, int)> GetAllAsync(int pageNumber, int pageSize);
    Task<User?> GetByIdAsync(int id);
    Task<User> CreateAsync(User entity);
    Task<User> UpdateAsync(User entity);
    Task<User> DeleteAsync(User entity);
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetEmailsAndUsernamesAsync(string email, string username);
    Task<bool> CheckIfUserIsAdminAsync(int userId);
}