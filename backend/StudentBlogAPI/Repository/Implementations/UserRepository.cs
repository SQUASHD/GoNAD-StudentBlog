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

    public async Task<(ICollection<User>, int)> GetAllAsync(int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;

        var users = await _dbContext.Users
            .OrderBy(u => u.Id)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        var totalUsers = await _dbContext.Users.CountAsync();

        return (users, totalUsers);
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

    public async Task<User> UpdateAsync(User entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<User> DeleteAsync(User entity)
    {
        _dbContext.Users.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
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