using Microsoft.EntityFrameworkCore;
using StudentBlogAPI.Data;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Repository.Interfaces;

namespace StudentBlogAPI.Repository;

public class UserRepository : IUserRepository
{
    private readonly StudentBlogDbContext _dbContext;

    public UserRepository(StudentBlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }


    public async Task<User?> GetByIdAsync(int id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<User> CreateAsync(User entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<User?> UpdateAsync(int id, User entity)
    {
        var existingUser = await _dbContext.Users.FindAsync(id);
        if (existingUser == null) return null;

        existingUser = entity;
        existingUser.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
        return existingUser;
    }

    public async Task<User?> DeleteAsync(int id)
    {
        var user = await _dbContext.Users.FindAsync(id);
        if (user == null) return null;
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }

    public async Task<User?> GetByUsernameAsync(string username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }

    public async Task<User?> GetEmailsAndUsernamesAsync(string email, string username)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email || u.UserName == username);
    }

    public async Task<bool> CheckIfUserIsAdminAsync(int userId)
    {
        var user = await _dbContext.Users.FindAsync(userId);
        return user is { IsAdmin: true };
    }
}