using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;

namespace StudentBlogAPI.Mappers.Interfaces;

public interface ICommentMapper
{
    CommentResDto MapToDto(Comment model);
    Comment MapCreateToModel(CreateCommentDto dto);
    Comment MapUpdateToModel(UpdateCommentDto dto);
    ICollection<CommentResDto> MapCollection(ICollection<Comment> models);
}