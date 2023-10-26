using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentBlogAPI.Extensions;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Services.Interfaces;

namespace StudentBlogAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly IUserService _userService;


    public UsersController(IUserService userService, IJwtService jwtService)
    {
        _userService = userService;
        _jwtService = jwtService;
    }

    [Authorize(Policy = "AdminOnly")]
    [HttpGet(Name = "GetUsers")]
    public async Task<ActionResult<ICollection<UserResDto>>> GetUsers()
    {
        var users = await _userService.GetAllAsync();
        return users.Any() ? Ok(users) : NoContent();
    }

    [Authorize]
    [HttpGet("{userId}", Name = "GetUserById")]
    public async Task<ActionResult<UserResDto>> GetUserById(int userId)
    {
        var user = await _userService.GetByIdAsync(userId);
        return user != null ? Ok(user) : NotFound();
    }

    [Authorize]
    [HttpPut("{userId}", Name = "UpdateUser")]
    public async Task<ActionResult<UserResDto>> UpdateUser(int userId, UpdateUserInputReqDto resDto)
    {
        var currentUserId = this.GetCurrentUserId(_jwtService);
        var updatedUser = await _userService.UpdateAsync(currentUserId, userId, resDto);

        return updatedUser != null ? Ok(updatedUser) : NotFound();
    }

    [Authorize]
    [HttpDelete("{userId}", Name = "DeleteUser")]
    public async Task<ActionResult<UserResDto>> DeleteUser(int userId)
    {
        var currentUserId = this.GetCurrentUserId(_jwtService);
        var deletedUser = await _userService.DeleteAsync(currentUserId, userId);

        return Ok(deletedUser);
    }
}