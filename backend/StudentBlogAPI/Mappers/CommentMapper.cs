using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;

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

    public Comment MapCreateToModel(CreateCommentDto dto)
    {
        return new Comment
        {
            PostId = dto.PostId,
            UserId = dto.UserId,
            Content = dto.Content
        };
    }

    public Comment MapUpdateToModel(UpdateCommentDto dto)
    {
        return new Comment
        {
            Id = dto.Id,
            UserId = dto.UserId,
            Content = dto.Content,
        };
    }

    public ICollection<CommentResDto> MapCollection(ICollection<Comment> models)
    {
        return models.Select(model => MapToDto(model)).ToList();
    }
}