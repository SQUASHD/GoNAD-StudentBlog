using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Mappers.Interfaces;

public interface IUserMapper
{
    UserResDto MapToResDto(User user);
    User MapToModel(InternalProcessedAuthData createData);

    ICollection<UserResDto> MapCollection(ICollection<User> models);
}