using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentBlogAPI.Extensions;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Services.Interfaces;

namespace StudentBlogAPI.Controllers;

[ApiController]
public class TokenController : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly ITokenValidator _tokenValidator;

    public TokenController(IJwtService jwtService, ITokenValidator tokenValidator)
    {
        _jwtService = jwtService;
        _tokenValidator = tokenValidator;
    }

    [AllowAnonymous]
    [HttpPost("api/v1/refresh", Name = "RefreshToken")]
    public async Task<ActionResult<AuthWithTokenResDto>> RefreshAccessToken()
    {
        var userId = this.GetCurrentUserId(_jwtService, _tokenValidator);
        var refreshToken = _jwtService.GetTokenFromRequest(HttpContext);
        var newAccessToken = await _jwtService.RefreshAccessToken(refreshToken, userId);
        return Ok(newAccessToken);
    }

    [AllowAnonymous]
    [HttpPost("api/v1/revoke", Name = "RevokeToken")]
    public async Task<ActionResult> RevokeToken()
    {
        var refreshToken = _jwtService.GetTokenFromRequest(HttpContext);
        await _jwtService.RevokeRefreshToken(refreshToken);
        return Ok();
    }
}