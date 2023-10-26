using StudentBlogAPI.Exceptions;
using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Repository.Interfaces;
using StudentBlogAPI.Services.Interfaces;

namespace StudentBlogAPI.Services;

public class CommentService : ICommentService
{
    private readonly ICommentMapper _commentMapper;
    private readonly IRepository<Comment> _commentRepository;

    public CommentService(ICommentMapper commentMapper, IRepository<Comment> commentRepository)
    {
        _commentMapper = commentMapper;
        _commentRepository = commentRepository;
    }


    public async Task<CommentResDto?> GetByIdAsync(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        return comment != null ? _commentMapper.MapToDto(comment) : null;
    }

    public async Task<CommentResDto?> CreateAsync(CreateCommentDto dto)
    {
        var comment = _commentMapper.MapCreateToModel(dto);
        var createdComment = await _commentRepository.CreateAsync(comment);
        return _commentMapper.MapToDto(createdComment);
    }

    public async Task<CommentResDto?> UpdateAsync(int currentUserId, int id, UpdateCommentDto dto)
    {
        var existingComment = await _commentRepository.GetByIdAsync(id);
        if (existingComment == null) throw new ItemNotFoundException();

        if (existingComment.UserId != currentUserId) throw new UserForbiddenException();

        var comment = _commentMapper.MapUpdateToModel(dto);
        var updatedComment = await _commentRepository.UpdateAsync(id, comment);
        return updatedComment != null ? _commentMapper.MapToDto(updatedComment) : null;
    }

    public async Task<CommentResDto?> DeleteAsync(int currentUserId, int id)
    {
        var existingComment = await _commentRepository.GetByIdAsync(id);
        if (existingComment == null) throw new ItemNotFoundException();

        if (existingComment.UserId != currentUserId) throw new UserForbiddenException();

        var deletedComment = await _commentRepository.DeleteAsync(id);
        return deletedComment != null ? _commentMapper.MapToDto(deletedComment) : null;
    }
}