using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using StudentBlogAPI.Exceptions;
using StudentBlogAPI.Repository.Interfaces;
using StudentBlogAPI.Services.Interfaces;

namespace StudentBlogAPI.Services.Implementations;

public class TokenValidator : ITokenValidator
{
    private readonly IConfiguration _config;
    private readonly IRevokedTokenRepository _revokedTokenRepository;

    public TokenValidator(IConfiguration config, IRevokedTokenRepository revokedTokenRepository)
    {
        _config = config;
        _revokedTokenRepository = revokedTokenRepository;
    }

    public int GetUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        if (tokenHandler.ReadToken(token) is not JwtSecurityToken jwtToken)
            throw new InvalidJwtException("The provided token is invalid or malformed.");

        var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "user_id");
        if (userIdClaim == null)
            throw new InvalidJwtException("The token does not contain a user ID claim.");

        if (!int.TryParse(userIdClaim.Value, out var userId))
            throw new InvalidJwtException("The user ID claim is not a valid integer.");

        return userId;
    }

    public async Task<bool> IsTokenRevoked(string token)
    {
        return await _revokedTokenRepository.IsTokenRevoked(token);
    }

    public async Task<bool> ValidateRefreshToken(string token)
    {
        var jwtKey = _config["Jwt:Key"];
        if (string.IsNullOrEmpty(jwtKey))
        {
            throw new InvalidOperationException("The JWT secret key is missing from the configuration.");
        }

        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = true,
            ValidIssuer = _config["Jwt:RefreshIssuer"],
            ValidateLifetime = true,
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        tokenHandler.ValidateToken(token, validationParameters, out var validatedToken);

        var isRevoked = await _revokedTokenRepository.IsTokenRevoked(token);
        if (isRevoked)
            throw new InvalidJwtException("The token has been revoked.");

        return validatedToken != null;
    }


}