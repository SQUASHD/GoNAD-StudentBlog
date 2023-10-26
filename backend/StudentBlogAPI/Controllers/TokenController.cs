using Microsoft.AspNetCore.Mvc;
using StudentBlogAPI.Extensions;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Services.Interfaces;

namespace StudentBlogAPI.Controllers;

[ApiController]
public class TokenController : ControllerBase
{
    private readonly IJwtService _jwtService;

    public TokenController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("api/v1/refresh", Name = "RefreshToken")]
    public async Task<ActionResult<AuthWithTokenResDto>> RefreshAccessToken()
    {
        var userId = this.GetCurrentUserId(_jwtService);
        var refreshToken = _jwtService.GetTokenFromRequest(HttpContext);
        var newAccessToken = await _jwtService.RefreshAccessToken(refreshToken, userId);
        return Ok(newAccessToken);
    }

    [HttpPost("api/v1/revoke", Name = "RevokeToken")]
    public async Task<ActionResult> RevokeToken()
    {
        var refreshToken = _jwtService.GetTokenFromRequest(HttpContext);
        await _jwtService.RevokeRefreshToken(refreshToken);
        return Ok();
    }
}