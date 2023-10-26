using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Mappers;

public class CommentMapper : ICommentMapper
{
    public CommentResDto MapToDto(Comment model)
    {
        return new CommentResDto(
            model.Id,
            model.PostId,
            model.UserId,
            model.Content,
            model.CreatedAt,
            model.UpdatedAt
        );
    }

    public Comment MapCreateToModel(InternalCreateCommentData data)
    {
        return new Comment
        {
            PostId = data.PostId,
            UserId = data.UserId,
            Content = data.Content
        };
    }

    public Comment MapUpdateToModel(InternalUpdateCommentData data)
    {
        return new Comment
        {
            Id = data.Id,
            UserId = data.UserId,
            Content = data.Content
        };
    }

    public ICollection<CommentResDto> MapCollection(ICollection<Comment> models)
    {
        return models.Select(model => MapToDto(model)).ToList();
    }
}