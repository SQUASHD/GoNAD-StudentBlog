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

    public async Task<AuthWithTokenResDto> RegisterAsync(UserRegisterReqDto reqDto)
    {
        var existingUser = await _userRepository.GetEmailsAndUsernamesAsync(reqDto.Email, reqDto.UserName);
        if (existingUser != null)
            throw new UserAlreadyExistsException();

        var salt = BCrypt.Net.BCrypt.GenerateSalt();
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(reqDto.Password, salt);

        var authCreateData = new InternalAuthData(
            reqDto.UserName,
            reqDto.FirstName,
            reqDto.LastName,
            reqDto.Email,
            hashedPassword,
            salt
        );

        var user = _userMapper.MapToModel(authCreateData);

        var createdUser = await _userRepository.CreateAsync(user);
        var accessToken = _jwtService.GenerateAccessToken(createdUser.Id);
        var refreshToken = _jwtService.GenerateRefreshToken(createdUser.Id);

        var authResDto = new AuthWithTokenResDto(
            createdUser.Id,
            createdUser.UserName,
            accessToken,
            refreshToken
        );

        return authResDto;
    }

    public async Task<AuthWithTokenResDto> LoginAsync(UserLoginReqDto payloadReqDto)
    {
        var userToLogin = await _userRepository.GetByUsernameAsync(payloadReqDto.UserName);
        if (userToLogin == null) throw new BadLoginRequestException();

        var oldSalt = userToLogin.Salt;
        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(payloadReqDto.Password, oldSalt);

        if (hashedPassword != userToLogin.HashedPassword)
            throw new BadLoginRequestException();

        var authResDto = _userMapper.MapToDto(userToLogin);
        var accessToken = _jwtService.GenerateAccessToken(userToLogin.Id);
        var refreshToken = _jwtService.GenerateRefreshToken(userToLogin.Id);

        var authResWithTokenDto = new AuthWithTokenResDto(
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