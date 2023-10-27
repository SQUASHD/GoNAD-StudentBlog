using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Mappers;

public class AuthMapper : IAuthMapper
{
    public InternalAuthResData MapToDto(User model)
    {
        return new InternalAuthResData(
            model.Id,
            model.UserName
        );
    }

    public User MapToModel(InternalProcessedAuthData dto)
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