using Microsoft.AspNetCore.Authorization;
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

    [AllowAnonymous]
    [HttpPost("login", Name = "Login")]
    public async Task<ActionResult<AuthWithTokenResDto>> Login(UserLoginReqDto userLoginReqDto)
    {
        var authWithTokenResDto = await _authService.LoginAsync(userLoginReqDto);
        return Ok(authWithTokenResDto);
    }
    
    [AllowAnonymous]
    [HttpPost("register", Name = "Register")]
    public async Task<ActionResult<AuthWithTokenResDto>> Register(UserRegisterReqDto registerReqDto)
    {
        var authWithTokenResDto = await _authService.RegisterAsync(registerReqDto);
        return Ok(authWithTokenResDto);
    }
    
    [HttpHead(Name = "IsAuthorized")]
    public ActionResult IsAuthorized()
    {
        return Ok();
    }
}