using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;

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

    public Post MapCreateToModel(InternalCreatePostData data)
    {
        return new Post
        {
            UserId = data.UserId,
            Title = data.Title,
            Content = data.Content
        };
    }

    public Post MapUpdateToModel(InternalUpdatePostData data)
    {
        return new Post
        {
            UserId = data.UserId,
            Title = data.Title,
            Content = data.Content
        };
    }

    public ICollection<PostResDto> MapCollection(ICollection<Post> models)
    {
        return models.Select(MapToDto).ToList();
    }
}