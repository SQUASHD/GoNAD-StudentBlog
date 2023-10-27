using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using StudentBlogAPI.Exceptions;
using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Internal;
using StudentBlogAPI.Repository.Interfaces;
using StudentBlogAPI.Services.Interfaces;

namespace StudentBlogAPI.Services.Implementations;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;
    private readonly ITokenMapper _tokenMapper;
    private readonly IRevokedTokenRepository _tokenRepository;
    private readonly ITokenValidator _tokenValidator;

    public JwtService(
        IConfiguration configuration,
        IRevokedTokenRepository tokenRepository,
        ITokenMapper tokenMapper,
        ITokenValidator tokenValidator)
    {
        _config = configuration;
        _tokenRepository = tokenRepository;
        _tokenMapper = tokenMapper;
        _tokenValidator = tokenValidator;
    }

    public string GenerateAccessToken(int userId)
    {
        var jwtKey = _config["Jwt:Key"];
        if (string.IsNullOrEmpty(jwtKey))
            throw new InvalidOperationException("The JWT secret key is missing from the configuration.");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("user_id", userId.ToString()) // Custom claim
        };

        var token = new JwtSecurityToken(
            _config["Jwt:AccessIssuer"],
            claims: claims,
            expires: DateTime.Now.AddHours(3), // Token expiry
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken(int userId)
    {
        var jwtKey = _config["Jwt:Key"];
        if (string.IsNullOrEmpty(jwtKey))
            throw new InvalidOperationException("The JWT secret key is missing from the configuration.");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim("user_id", userId.ToString())
        };

        var token = new JwtSecurityToken(
            _config["Jwt:RefreshIssuer"],
            claims: claims,
            expires: DateTime.Now.AddDays(24),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<AccessTokenResDto> RefreshAccessToken(string refreshToken, int userId)
    {
        var valid = await _tokenValidator.ValidateRefreshToken(refreshToken);
        if (!valid) throw new InvalidJwtException("Invalid refresh token");

        var accessToken = GenerateAccessToken(userId);
        return new AccessTokenResDto(accessToken);
    }

    public string GetTokenFromRequest(HttpContext httpContext)
    {
        var header = httpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrEmpty(header) || !header.StartsWith("Bearer "))
            throw new InvalidJwtException("No token found in header.");

        var token = header.Substring("Bearer ".Length).Trim();

        return token;
    }

    public async Task<bool> RevokeRefreshToken(string token)
    {
        var userId = _tokenValidator.GetUserIdFromToken(token);

        var internalData = new InternalRevokeTokenData(token, userId);
        var revokedTokenData = _tokenMapper.MapToModel(internalData);
        var isRevoked = await _tokenRepository.RevokeToken(revokedTokenData);
        return isRevoked;
    }
}