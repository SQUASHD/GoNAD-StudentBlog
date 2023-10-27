using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Mappers.Interfaces;

public interface IAuthMapper
{
    InternalAuthResData MapToDto(User model);
    User MapToModel(InternalProcessedAuthData dto);
}