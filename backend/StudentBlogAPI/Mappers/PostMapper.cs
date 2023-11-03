using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Mappers;

public class PostMapper : IPostMapper
{
    public PostResDto MapToResDto(Post model)
    {
        return new PostResDto(
            model.Id,
            model.UserId,
            model.Title,
            model.Content,
            model.Status,
            model.CreatedAt,
            model.UpdatedAt
        );
    }

    public Post MapToModel(InternalCreatePostData data)
    {
        return new Post
        {
            UserId = data.CurrentUserId,
            Title = data.Title,
            Content = data.Content
        };
    }

    public ICollection<PostResDto> MapCollection(ICollection<Post> models)
    {
        return models.Select(MapToResDto).ToList();
    }

    public Post MapUpdateToModel(InternalUpdatePostData data)
    {
        return new Post
        {
            UserId = data.CurrenUserId,
            Title = data.Title,
            Content = data.Content
        };
    }
}