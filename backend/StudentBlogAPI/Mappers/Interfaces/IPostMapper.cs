using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Mappers.Interfaces;

public interface IPostMapper
{
    PostResDto MapToResDto(Post model);

    Post MapToModel(InternalCreatePostData data);
    ICollection<PostResDto> MapCollection(ICollection<Post> models);
}