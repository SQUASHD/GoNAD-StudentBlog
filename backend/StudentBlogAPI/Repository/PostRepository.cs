using Microsoft.EntityFrameworkCore;
using StudentBlogAPI.Data;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Repository.Interfaces;

namespace StudentBlogAPI.Repository;

public class PostRepository : IRepository<Post>
{
    private readonly StudentBlogDbContext _dbContext;

    public PostRepository(StudentBlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<Post>> GetAllAsync()
    {
        return await _dbContext.Posts.ToListAsync();
    }

    public async Task<Post?> GetByIdAsync(int id)
    {
        return await _dbContext.Posts.FindAsync(id);
    }

    public async Task<Post> CreateAsync(Post entity)
    {
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Post> UpdateAsync(Post entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Post> DeleteAsync(Post entity)
    {
        _dbContext.Posts.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
}