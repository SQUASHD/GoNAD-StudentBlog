using StudentBlogAPI.Model.DTOs;

namespace StudentBlogAPI.Services.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(int userId);
    string GenerateRefreshToken(int userId);
    Task<AccessTokenResDto> RefreshAccessToken(string refreshToken, int userId);
    string GetTokenFromRequest(HttpContext httpContext);
    Task<bool> RevokeRefreshToken(string token);
}