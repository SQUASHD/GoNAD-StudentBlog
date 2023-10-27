using Microsoft.EntityFrameworkCore;
using StudentBlogAPI.Data;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Repository.Interfaces;

namespace StudentBlogAPI.Repository;

public class CommentRepository : ICommentRepository
{
    private readonly StudentBlogDbContext _dbContext;

    public CommentRepository(StudentBlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(ICollection<Comment>, int)> GetAllAsync(int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;

        var comments = await _dbContext.Comments
            .OrderBy(c => c.Id)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        var totalComments = await _dbContext.Comments.CountAsync();

        return (comments, totalComments);
    }

    public async Task<ICollection<Comment>> GetCommentsByPostIdAsync(int postId)
    {
        var commentsQuery = _dbContext.Comments.Where(c => c.PostId == postId);
        var comments = await commentsQuery.ToListAsync();
        return comments;
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