using Microsoft.AspNetCore.Mvc;
using StudentBlogAPI.Extensions;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Internal;
using StudentBlogAPI.Services.Interfaces;
using StudentBlogAPI.Utilities;

namespace StudentBlogAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly IUserService _userService;
    private readonly ITokenValidator _tokenValidator;


    public UsersController(IUserService userService, IJwtService jwtService, ITokenValidator tokenValidator)
    {
        _userService = userService;
        _jwtService = jwtService;
        _tokenValidator = tokenValidator;
    }

    [HttpGet(Name = "GetUsers")]
    public async Task<ActionResult<PaginatedResultDto<UserResDto>>> GetUsers([FromQuery] int page = 1,
        [FromQuery] int size = 10)
    {
        if (page < 1 || size < 1)
            return BadRequest("Page and size parameters must be greater than 0");

        var paginatedUsers = await _userService.GetAllAsync(page, size);

        return Ok(paginatedUsers);
    }

    [HttpGet("{userId}", Name = "GetUserById")]
    public async Task<ActionResult<UserResDto>> GetUserById(int userId)
    {
        var user = await _userService.GetByIdAsync(userId);
        return user != null ? Ok(user) : NotFound();
    }

    [HttpPut("{userId}", Name = "UpdateUserInfo")]
    public async Task<ActionResult<UserResDto>> UpdateUserInfo(int userId, UpdateUserInfoReqDto resDto)
    {
        var currentUserId = this.GetCurrentUserId(_jwtService, _tokenValidator);

        var data = new InternalUpdateUserInfoData(
            currentUserId,
            userId,
            resDto.FirstName,
            resDto.LastName
        );

        var updatedUser = await _userService.UpdateUserInfoAsync(data);

        return Ok(updatedUser);
    }

    [HttpPut("{userId}/password", Name = "UpdateUserPassword")]
    public async Task<ActionResult<UserResDto>> UpdateUserPassword(int userId, UpdatePasswordReqDto resDto)
    {
        var currentUserId = this.GetCurrentUserId(_jwtService, _tokenValidator);

        var data = new InternalUpdatePasswordReqData(
            currentUserId,
            userId,
            resDto.OldPassword,
            resDto.NewPassword
        );

        var updatedUser = await _userService.UpdatePasswordAsync(data);

        return Ok(updatedUser);
    }

    [HttpDelete("{userId}", Name = "DeleteUser")]
    public async Task<ActionResult<UserResDto>> DeleteUser(int userId)
    {
        var currentUserId = this.GetCurrentUserId(_jwtService, _tokenValidator);

        var internalDeleteUserData = new InternalDeleteUserData(
            currentUserId,
            userId
        );

        var deletedUser = await _userService.DeleteAsync(internalDeleteUserData);

        return Ok(deletedUser);
    }
}