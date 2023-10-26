using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;

namespace StudentBlogAPI.Mappers;

public class UserMapper : IUserMapper
{
    public UserResDto MapToDto(User user)
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

    public User MapUpdateToModel(UpdateUserData data)
    {
        return new User
        {
            FirstName = data.FirstName,
            LastName = data.LastName,
            HashedPassword = data.HashedNewPassword,
            Salt = data.NewSalt
        };
    }


    public ICollection<UserResDto> MapCollection(ICollection<User> models)
    {
        return models.Select(MapToDto).ToList();
    }
}