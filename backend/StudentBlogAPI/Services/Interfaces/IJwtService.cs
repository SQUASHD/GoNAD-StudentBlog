using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using StudentBlogAPI.Model.DTOs;

namespace StudentBlogAPI.Services.Interfaces;

public interface IJwtService
{
    string GenerateAccessToken(int userId);
    string GenerateRefreshToken(int userId);
    Task<RefreshAccessTokenDto> RefreshAccessToken(string refreshToken, int userId);
    int GetUserIdFromToken(string token);
    string GetTokenFromRequest(HttpContext httpContext);
    Task<bool> RevokeRefreshToken(string token);
    Task<bool> ValidateRefreshToken(string token);
}