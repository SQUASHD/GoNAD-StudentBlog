using Microsoft.EntityFrameworkCore;
using StudentBlogAPI.Data;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Repository.Interfaces;

namespace StudentBlogAPI.Repository;

public class CommentRepository : IRepository<Comment>
{
    private readonly StudentBlogDbContext _dbContext;

    public CommentRepository(StudentBlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ICollection<Comment>> GetAllAsync()
    {
        return await _dbContext.Comments.ToListAsync();
    }

    public async Task<Comment?> GetByIdAsync(int id)
    {
        return await _dbContext.Comments.FindAsync(id);
    }

    public async Task<Comment> CreateAsync(Comment entity)
    {
        await _dbContext.Comments.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Comment> UpdateAsync(Comment entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<Comment> DeleteAsync(Comment entity)
    {
        _dbContext.Comments.Remove(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }
}