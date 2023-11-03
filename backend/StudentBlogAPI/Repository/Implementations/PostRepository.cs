using Microsoft.EntityFrameworkCore;
using StudentBlogAPI.Data;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;
using StudentBlogAPI.Repository.Interfaces;

namespace StudentBlogAPI.Repository.Implementations;

public class PostRepository : IPostRepository
{
    private readonly StudentBlogDbContext _dbContext;

    public PostRepository(StudentBlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(ICollection<Post>, int)> GetAllAsync(InternalGetAllPostsData data)
    {
        var skip = (data.PageNumber - 1) * data.PageSize;

        ICollection<Post> posts;

        if (data.OrderBy == "asc")
            posts = await _dbContext.Posts
                .Where(p => p.Status == PublicationStatus.Published)
                .OrderBy(p => p.Id)
                .Skip(skip)
                .Take(data.PageSize)
                .ToListAsync();
        else
            posts = await _dbContext.Posts
                .Where(p => p.Status == PublicationStatus.Published)
                .OrderByDescending(p => p.Id)
                .Skip(skip)
                .Take(data.PageSize)
                .ToListAsync();

        var totalComments = await _dbContext.Posts.CountAsync();

        return (posts, totalComments);
        ;
    }

    public async Task<(ICollection<Post>, int)> GetPostsByUserIdAsync(int userId, int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;
        var posts = await _dbContext.Posts
            .Where(p => p.UserId == userId)
            .OrderBy(p => p.Id)
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();
        var totalPosts = await _dbContext.Posts
            .Where(p => p.UserId == userId)
            .CountAsync();

        return (posts, totalPosts);
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