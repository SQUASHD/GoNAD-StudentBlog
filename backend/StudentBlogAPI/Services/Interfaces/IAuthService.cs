using StudentBlogAPI.Model.DTOs;

namespace StudentBlogAPI.Services.Interfaces;

public interface IAuthService
{
    Task<AuthWithTokenResDto> RegisterAsync(UserRegisterReqDto reqDto);
    Task<AuthWithTokenResDto> LoginAsync(UserLoginReqDto reqDto);
    Task<bool> CheckIfUserIsAdminAsync(int userId);
}