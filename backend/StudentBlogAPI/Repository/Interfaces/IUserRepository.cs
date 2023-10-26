using StudentBlogAPI.Model.Entities;

namespace StudentBlogAPI.Repository.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByUsernameAsync(string username);
    Task<User?> GetEmailsAndUsernamesAsync(string email, string username);
    Task<bool> CheckIfUserIsAdminAsync(int userId);
}