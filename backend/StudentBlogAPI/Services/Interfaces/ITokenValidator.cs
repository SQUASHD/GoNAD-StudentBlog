namespace StudentBlogAPI.Services.Interfaces;

public interface ITokenValidator
{
    Task<bool> ValidateRefreshToken(string token);
    Task<bool> IsTokenRevoked(string token);
    int GetUserIdFromToken(string token);
}