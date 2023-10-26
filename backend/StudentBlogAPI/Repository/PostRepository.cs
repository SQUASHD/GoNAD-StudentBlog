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

    public async Task<Post?> UpdateAsync(int id, Post entity)
    {
        try
        {
            var existingPost = await _dbContext.Posts.FindAsync(id);
            if (existingPost == null) return null;

            existingPost.Title = entity.Title;
            existingPost.Content = entity.Content;
            existingPost.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();
            return existingPost;
        }
        catch (DbUpdateException ex)
        {
            throw new InvalidOperationException("Could not update the post", ex);
        }
    }

    public async Task<Post?> DeleteAsync(int id)
    {
        var post = await _dbContext.Posts.FindAsync(id);
        if (post == null) return null;
        _dbContext.Posts.Remove(post);
        await _dbContext.SaveChangesAsync();
        return post;
    }
}