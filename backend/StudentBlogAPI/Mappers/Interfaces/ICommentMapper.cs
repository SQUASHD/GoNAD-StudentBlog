using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Mappers.Interfaces;

public interface ICommentMapper
{
    CommentResDto MapToDto(Comment model);
    Comment MapCreateToModel(InternalCreateCommentData data);
    Comment MapUpdateToModel(InternalUpdateCommentData data);
    ICollection<CommentResDto> MapCollection(ICollection<Comment> models);
}