using StudentBlogAPI.Model.DTOs;

namespace StudentBlogAPI.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResWithTokenDto> RegisterAsync(UserRegisterDto dto);
    Task<AuthResWithTokenDto> LoginAsync(UserLoginDto dto);
    Task<bool> CheckIfUserIsAdminAsync(int userId);
}