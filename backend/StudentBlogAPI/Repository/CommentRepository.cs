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

    public async Task<Comment?> UpdateAsync(int id, Comment entity)
    {
        var existingComment = await _dbContext.Comments.FindAsync(id);
        if (existingComment == null) return null;

        existingComment.Content = entity.Content;
        existingComment.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
        return existingComment;
    }

    public async Task<Comment?> DeleteAsync(int id)
    {
        var comment = await _dbContext.Comments.FindAsync(id);
        if (comment == null) return null;
        _dbContext.Comments.Remove(comment);
        await _dbContext.SaveChangesAsync();
        return comment;
    }
}