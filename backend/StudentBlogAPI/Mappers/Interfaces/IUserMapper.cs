using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;

namespace StudentBlogAPI.Mappers.Interfaces;

public interface IUserMapper
{
    UserResDto MapToDto(User user);
    User MapUpdateToModel(UpdateUserData updateUserData);
    ICollection<UserResDto> MapCollection(ICollection<User> models);
}