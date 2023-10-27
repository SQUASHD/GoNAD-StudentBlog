using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Mappers.Interfaces;

public class TokenMapper : ITokenMapper
{
    public RevokedToken MapToModel(InternalRevokeTokenData dto)
    {
        return new RevokedToken
        {
            Token = dto.Token,
            UserId = dto.UserId
        };
    }
}