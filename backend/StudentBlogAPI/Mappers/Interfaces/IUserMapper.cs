using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Mappers.Interfaces;

public interface IUserMapper
{
    UserResDto MapToDto(User user);
    User MapUpdateToModel(InternalUpdateUserData internalUpdateUserData);
    ICollection<UserResDto> MapCollection(ICollection<User> models);
}