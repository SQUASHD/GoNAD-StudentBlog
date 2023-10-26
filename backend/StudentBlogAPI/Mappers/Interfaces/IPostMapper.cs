using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Mappers.Interfaces;

public interface IPostMapper
{
    PostResDto MapToDto(Post model);

    Post MapCreateToModel(InternalCreatePostData data);
    Post MapUpdateToModel(InternalUpdatePostData data);
    ICollection<PostResDto> MapCollection(ICollection<Post> models);
}