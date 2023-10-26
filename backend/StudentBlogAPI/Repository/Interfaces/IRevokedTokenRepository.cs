using StudentBlogAPI.Model.Entities;

namespace StudentBlogAPI.Repository.Interfaces;

public interface IRevokedTokenRepository
{
    Task<bool> IsTokenRevoked(string token);
    Task<bool> RevokeToken(RevokedToken entity);
}