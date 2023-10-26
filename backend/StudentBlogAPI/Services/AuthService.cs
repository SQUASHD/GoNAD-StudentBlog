using StudentBlogAPI.Exceptions;
using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Internal;
using StudentBlogAPI.Repository.Interfaces;
using StudentBlogAPI.Services.Interfaces;

namespace StudentBlogAPI.Services;

public class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly IAuthMapper _userMapper;
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository, IAuthMapper userMapper, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _userMapper = userMapper;
        _jwtService = jwtService;
    }

    public async Task<AuthResWithTokenDto> RegisterAsync(UserRegisterDto dto)
    {
        var existingUser = await _userRepository.GetEmailsAndUsernamesAsync(dto.Email, dto.UserName);
        if (existingUser != null)
            throw new UserAlreadyExistsException();

        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password, salt);

        var authCreateData = new AuthInternalDto(
            dto.UserName,
            dto.FirstName,
            dto.LastName,
            dto.Email,
            hashedPassword,
            salt
        );

        var user = _userMapper.MapToModel(authCreateData);

        var createdUser = await _userRepository.CreateAsync(user);
        var accessToken = _jwtService.GenerateAccessToken(createdUser.Id);
        var refreshToken = _jwtService.GenerateRefreshToken(createdUser.Id);

        var authResDto = new AuthResWithTokenDto(
            createdUser.Id,
            createdUser.UserName,
            accessToken,
            refreshToken
        );

        return authResDto;
    }

    public async Task<AuthResWithTokenDto> LoginAsync(UserLoginDto payloadDto)
    {
        var userToLogin = await _userRepository.GetByUsernameAsync(payloadDto.UserName);
        if (userToLogin == null) throw new BadLoginRequestException();

        var oldSalt = userToLogin.Salt;
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(payloadDto.Password, oldSalt);

        if (hashedPassword != userToLogin.HashedPassword)
            throw new BadLoginRequestException();

        var authResDto = _userMapper.MapToDto(userToLogin);
        var accessToken = _jwtService.GenerateAccessToken(userToLogin.Id);
        var refreshToken = _jwtService.GenerateRefreshToken(userToLogin.Id);

        var authResWithTokenDto = new AuthResWithTokenDto(
            authResDto.Id,
            authResDto.UserName,
            accessToken,
            refreshToken
            );

        return authResWithTokenDto;
    }

    public Task<bool> CheckIfUserIsAdminAsync(int userId)
    {
        return _userRepository.CheckIfUserIsAdminAsync(userId);
    }
}