using Microsoft.EntityFrameworkCore;
using StudentBlogAPI.Data;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Repository.Interfaces;

namespace StudentBlogAPI.Repository;

public class PostRepository : IPostRepository
{
    private readonly StudentBlogDbContext _dbContext;

    public PostRepository(StudentBlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(ICollection<Post>, int)> GetAllAsync(int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;

        var posts = await _dbContext.Posts
            .OrderBy(p => p.Id)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        var totalComments = await _dbContext.Posts.CountAsync();

        return (posts, totalComments);
        ;
    }

    public async Task<ICollection<Post>> GetPostsByUserIdAsync(int userId)
    {
        var postsQuery = _dbContext.Posts.Where(p => p.UserId == userId);
        var posts = await postsQuery.ToListAsync();
        return posts;
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