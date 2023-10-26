using StudentBlogAPI.Exceptions;
using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Internal;
using StudentBlogAPI.Repository.Interfaces;
using StudentBlogAPI.Services.Interfaces;

namespace StudentBlogAPI.Services;

public class UserService : IUserService
{
    private readonly IUserMapper _userMapper;
    private readonly IUserRepository _userRepository;

    public UserService(IUserMapper userMapper, IUserRepository userRepository)
    {
        _userMapper = userMapper;
        _userRepository = userRepository;
    }

    public async Task<ICollection<UserResDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _userMapper.MapCollection(users);
    }

    public async Task<UserResDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user != null ? _userMapper.MapToDto(user) : null;
    }

    public async Task<UserResDto?> UpdateAsync(int currentUserId, int id, UpdateUserInputReqDto inputReqDto)
    {
        var userToUpdate = await _userRepository.GetByIdAsync(id);
        if (userToUpdate == null) throw new ItemNotFoundException("The requested user was not found");

        if (userToUpdate.Id != currentUserId || !userToUpdate.IsAdmin) throw new UserForbiddenException();
        var newSalt = BCrypt.Net.BCrypt.GenerateSalt();
        var oldSalt = userToUpdate.Salt;
        var hashedOldPassword = BCrypt.Net.BCrypt.HashPassword(inputReqDto.OldPassword, oldSalt);

        if (hashedOldPassword != userToUpdate.HashedPassword)
            throw new PasswordMismatchException("Old password does not match");

        var newHashedPassword = BCrypt.Net.BCrypt.HashPassword(inputReqDto.NewPassword, newSalt);
        
        var updateUserData = new InternalUpdateUserData(
            userToUpdate.Id,
            inputReqDto.FirstName,
            inputReqDto.LastName,
            newHashedPassword,
            newSalt
        );
        
        userToUpdate = _userMapper.MapUpdateToModel(updateUserData);

        var updatedUser = await _userRepository.UpdateAsync(id, userToUpdate);
        return updatedUser != null ? _userMapper.MapToDto(updatedUser) : null;
    }

    public async Task<UserResDto?> DeleteAsync(int currentUserId, int userIdToDelete)
    {
        var currentUser = await _userRepository.GetByIdAsync(currentUserId);
        if (currentUser == null) throw new UserNotAuthorizedException();

        var isAuthorizedToDelete =
            currentUser.IsAdmin ||
            currentUser.Id == userIdToDelete;

        if (!isAuthorizedToDelete)
            throw new UserForbiddenException();

        var userToDelete = await _userRepository.DeleteAsync(userIdToDelete);
        if (userToDelete == null) throw new ItemNotFoundException($"User with ID {userIdToDelete} not found.");

        return _userMapper.MapToDto(userToDelete);
    }
}