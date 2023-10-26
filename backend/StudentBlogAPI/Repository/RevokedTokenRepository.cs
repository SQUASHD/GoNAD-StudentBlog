using Microsoft.EntityFrameworkCore;
using StudentBlogAPI.Data;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Repository.Interfaces;

namespace StudentBlogAPI.Repository;

public class RevokedTokenRepository : IRevokedTokenRepository
{
    private readonly StudentBlogDbContext _dbContext;
    
    public RevokedTokenRepository(StudentBlogDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> IsTokenRevoked(string token)
    {
        var revokedToken = await _dbContext.RevokedTokens.FirstOrDefaultAsync(rt => rt.Token == token);
        return revokedToken != null;
    }

    public async Task<bool> RevokeToken(RevokedToken entity)
    {
        await _dbContext.RevokedTokens.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}