using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Mappers.Interfaces;

public interface ITokenMapper
{
     RevokedToken DtoToModel(InternalRevokeTokenData dto);
}