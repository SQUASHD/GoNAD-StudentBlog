using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Mappers.Interfaces;

public interface ICommentMapper
{
    CommentResDto MapToResDto(Comment entity);
    Comment MapToModel(InternalCreateCommentData data);
    ICollection<CommentResDto> MapCollection(ICollection<Comment> models);
}