using Microsoft.AspNetCore.Mvc;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Services.Interfaces;

namespace StudentBlogAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("login", Name = "Login")]
    public async Task<ActionResult<AuthResWithTokenDto>> Login(UserLoginDto userLoginDto)
    {
        var authResWithTokenDto = await _authService.LoginAsync(userLoginDto);
        return Ok(authResWithTokenDto);
    }

    [HttpPost("register", Name = "Register")]
    public async Task<ActionResult<AuthResWithTokenDto>> Register(UserRegisterDto registerDto)
    {
        var authResWithTokenDto = await _authService.RegisterAsync(registerDto);
        return Ok(authResWithTokenDto);
    }
}