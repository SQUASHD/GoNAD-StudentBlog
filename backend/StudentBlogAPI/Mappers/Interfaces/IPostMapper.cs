using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;

namespace StudentBlogAPI.Mappers.Interfaces;

public interface IPostMapper
{
    PostResDto MapToDto(Post model);

    Post MapCreateToModel(CreatePostDto dto);
    Post MapUpdateToModel(UpdatePostDto dto);
    ICollection<PostResDto> MapCollection(ICollection<Post> models);
}