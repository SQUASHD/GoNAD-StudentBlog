using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;

namespace StudentBlogAPI.Mappers;

public class PostMapper : IPostMapper
{
    public PostResDto MapToDto(Post model)
    {
        return new PostResDto(
            model.Id,
            model.UserId,
            model.Title,
            model.Content,
            model.CreatedAt,
            model.UpdatedAt
        );
    }

    public Post MapCreateToModel(CreatePostDto dto)
    {
        return new Post
        {
            UserId = dto.UserId,
            Title = dto.Title,
            Content = dto.Content
        };
    }

    public Post MapUpdateToModel(UpdatePostDto dto)
    {
        return new Post
        {
            UserId = dto.UserId,
            Title = dto.Title,
            Content = dto.Content
        };
    }

    public ICollection<PostResDto> MapCollection(ICollection<Post> models)
    {
        return models.Select(MapToDto).ToList();
    }
}