using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Mappers;

internal class CommentMapper : ICommentMapper
{
    public CommentResDto MapToResDto(Comment entity)
    {
        return new CommentResDto(
            entity.Id,
            entity.PostId,
            entity.UserId,
            entity.Content,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }

    public Comment MapToModel(InternalCreateCommentData createData)
    {
        return new Comment
        {
            PostId = createData.PostId,
            UserId = createData.CurrentUserId,
            Content = createData.Content
        };
    }

    public ICollection<CommentResDto> MapCollection(ICollection<Comment> entities)
    {
        return entities.Select(MapToResDto).ToList();
    }
}