using StudentBlogAPI.Exceptions;
using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Internal;
using StudentBlogAPI.Repository.Interfaces;
using StudentBlogAPI.Services.Interfaces;
using StudentBlogAPI.Utilities;

namespace StudentBlogAPI.Services;

public class UserService : IUserService
{
    private readonly IPostMapper _postMapper;
    private readonly IPostRepository _postRepository;
    private readonly IUserMapper _userMapper;
    private readonly IUserRepository _userRepository;

    public UserService(IUserMapper userMapper, IUserRepository userRepository, IPostRepository postRepository,
        IPostMapper postMapper)
    {
        _userMapper = userMapper;
        _userRepository = userRepository;
        _postRepository = postRepository;
        _postMapper = postMapper;
    }

    public async Task<PaginatedResultDto<UserResDto>> GetAllAsync(int pageNumber, int pageSize)
    {
        var (users, totalUsers) = await _userRepository.GetAllAsync(pageNumber, pageSize);

        if (users == null)
            throw new ItemNotFoundException("Users not found");

        var userDtos = _userMapper.MapCollection(users);

        return new PaginatedResultDto<UserResDto>(userDtos, pageNumber, pageSize, totalUsers);
    }

    public async Task<UserResDto?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) throw new ItemNotFoundException($"User with ID {id} not found.");
        return _userMapper.MapToResDto(user);
    }

    public async Task<ICollection<PostResDto>> GetPostsByUserIdAsync(int userId)
    {
        var posts = await _postRepository.GetPostsByUserIdAsync(userId);
        if (posts == null) throw new ItemNotFoundException($"No posts by user with {userId}");
        return _postMapper.MapCollection(posts);
    }

    public async Task<UserResDto?> UpdateUserInfoAsync(InternalUpdateUserInfoData data)
    {
        var userToUpdate = await _userRepository.GetByIdAsync(data.UserToUpdateId);
        if (userToUpdate == null) throw new ItemNotFoundException("The requested user was not found");

        if (userToUpdate.Id != data.CurrentUserId) throw new UserForbiddenException();

        userToUpdate.FirstName = data.FirstName;
        userToUpdate.LastName = data.LastName;

        var user = await _userRepository.UpdateAsync(userToUpdate);

        var updatedUserResDto = _userMapper.MapToResDto(user);

        return updatedUserResDto;
    }

    public async Task<UserResDto?> UpdatePasswordAsync(InternalUpdatePasswordReqData data)
    {
        var userToUpdate = await _userRepository.GetByIdAsync(data.UserToUpdateId);
        if (userToUpdate == null) throw new ItemNotFoundException("The requested user was not found");

        if (userToUpdate.Id != data.CurrentUserId || !userToUpdate.IsAdmin) throw new UserForbiddenException();

        var oldSalt = userToUpdate.Salt;
        var hashedOldPassword = BCrypt.Net.BCrypt.HashPassword(data.OldPassword, oldSalt);

        if (hashedOldPassword != userToUpdate.HashedPassword)
            throw new PasswordMismatchException("Passwords do not match");

        var newSalt = BCrypt.Net.BCrypt.GenerateSalt();
        var newHashedPassword = BCrypt.Net.BCrypt.HashPassword(data.NewPassword, newSalt);

        var processedData = new InternalProcessedUpdatePasswordData(
            newHashedPassword,
            newSalt
        );

        userToUpdate.HashedPassword = processedData.HashedNewPassword;
        userToUpdate.Salt = processedData.NewSalt;

        var user = await _userRepository.UpdateAsync(userToUpdate);

        var updatedUserRestDto = _userMapper.MapToResDto(user);

        return updatedUserRestDto;
    }

    public async Task<UserResDto?> DeleteAsync(InternalDeleteUserData data)
    {
        var currentUser = await _userRepository.GetByIdAsync(data.CurrentUserId);
        var userToDelete = await _userRepository.GetByIdAsync(data.UserToDeleteId);

        if (currentUser == null) throw new UserNotAuthorizedException();
        if (userToDelete == null) throw new ItemNotFoundException($"User with ID {data.UserToDeleteId} not found.");

        var isAuthorizedToDelete =
            currentUser.IsAdmin ||
            currentUser.Id == userToDelete.Id;

        if (!isAuthorizedToDelete) throw new UserForbiddenException();

        var deletedUser = await _userRepository.DeleteAsync(userToDelete);

        return _userMapper.MapToResDto(deletedUser);
    }
}