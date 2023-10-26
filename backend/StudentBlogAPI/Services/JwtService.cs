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

namespace StudentBlogAPI.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _config;
    private readonly IRevokedTokenRepository _tokenRepository;
    private readonly ITokenMapper _tokenMapper;

    public JwtService(IConfiguration configuration, IRevokedTokenRepository tokenRepository, ITokenMapper tokenMapper)
    {
        _config = configuration;
        _tokenRepository = tokenRepository;
        _tokenMapper = tokenMapper;
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
            new Claim("user_id", userId.ToString()) // Custom claim
        };
        
        var token = new JwtSecurityToken(
            _config["Jwt:RefreshIssuer"],
            claims: claims,// Replace with your app's name
            expires: DateTime.Now.AddDays(24), // Token expiry
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<RefreshAccessTokenDto> RefreshAccessToken(string refreshToken, int userId)
    {
        var valid = await ValidateRefreshToken(refreshToken);
        if (!valid) throw new InvalidJwtException("Invalid refresh token");
        var accessToken = GenerateAccessToken(userId);
        
        return new RefreshAccessTokenDto(accessToken);

    }

    public int GetUserIdFromToken(string token)
    {
        var jwtKey = _config["Jwt:Key"];
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new InvalidOperationException("The JWT secret key is missing from the configuration.");
        }

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateLifetime = false 
        };

        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out _);

        var userIdClaim = principal.FindFirst(claim => claim.Type == "user_id");
        if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out var userId))
        {
            throw new InvalidOperationException("Invalid token: user_id claim not found or invalid.");
        }

        return userId;
    }

    public string GetTokenFromRequest(HttpContext httpContext)
    {
        var header = httpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrEmpty(header) || !header.StartsWith("Bearer "))
        {
            throw new InvalidJwtException("No token found in header.");
        }

        var token = header.Substring("Bearer ".Length).Trim();
        
        return token;
    }

    public async Task<bool> RevokeRefreshToken(string token)
    {
        var userId = GetUserIdFromToken(token);
        
        var internalData = new RevokeDataInternalDto(
            token,
            userId
        );
        var revokedTokenData = _tokenMapper.DtoToModel(internalData);
        var isRevoked = await _tokenRepository.RevokeToken(revokedTokenData);
        return isRevoked;
    }

    public async Task<bool> ValidateRefreshToken(string token)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var signingKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? throw new InvalidOperationException()));
        
        jwtHandler.ValidateToken(token, new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = signingKey, 
            ValidateIssuer = true,
            ValidIssuer = _config["Jwt:RefreshIssuer"],
            ValidateAudience = false,
            ValidateLifetime = true
        }, out var validatedToken);
        
        var isRevoked = await _tokenRepository.IsTokenRevoked(token);
        if (isRevoked) return false;
        
        return validatedToken != null;
    }
}