using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Mappers;

public class AuthMapper : IAuthMapper
{
    public AuthResDto MapToDto(User model)
    {
        return new AuthResDto(
            model.Id,
            model.UserName
        );
    }

    public User MapToModel(InternalAuthData dto)
    {
        return new User
        {
            UserName = dto.UserName,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            HashedPassword = dto.HashedPassword,
            Salt = dto.Salt
        };
    }
}