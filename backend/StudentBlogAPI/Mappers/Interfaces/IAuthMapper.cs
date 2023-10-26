using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Mappers.Interfaces;

public interface IAuthMapper
{
    AuthResDto MapToDto(User model);
    User MapToModel(InternalAuthData dto);
}