using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Mappers;

public class UserMapper : IUserMapper
{
    public UserResDto MapToResDto(User user)
    {
        return new UserResDto(
            user.Id,
            user.UserName,
            user.FirstName,
            user.LastName,
            user.Email,
            user.CreatedAt,
            user.UpdatedAt
        );
    }

    public User MapToModel(InternalProcessedAuthData createData)
    {
        return new User
        {
            UserName = createData.UserName,
            FirstName = createData.FirstName,
            LastName = createData.LastName,
            Email = createData.Email,
            HashedPassword = createData.HashedPassword,
            Salt = createData.Salt
        };
    }

    public ICollection<UserResDto> MapCollection(ICollection<User> models)
    {
        return models.Select(MapToResDto).ToList();
    }
}