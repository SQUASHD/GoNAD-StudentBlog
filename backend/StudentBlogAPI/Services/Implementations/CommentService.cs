using StudentBlogAPI.Exceptions;
using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Internal;
using StudentBlogAPI.Repository.Interfaces;
using StudentBlogAPI.Services.Interfaces;
using StudentBlogAPI.Utilities;

namespace StudentBlogAPI.Services;

public class CommentService : ICommentService
{
    private readonly ICommentMapper _commentMapper;
    private readonly ICommentRepository _commentRepository;
    private readonly IPostRepository _postRepository;

    public CommentService(ICommentMapper commentMapper, ICommentRepository commentRepository, IPostRepository postRepository)
    {
        _commentMapper = commentMapper;
        _commentRepository = commentRepository;
        _postRepository = postRepository;
    }

    public async Task<PaginatedResultDto<CommentResDto>> GetAllAsync(int pageNumber, int pageSize)
    {
        var (comments, totalPosts) = await _commentRepository.GetAllAsync(pageNumber, pageSize);

        if (comments == null)
            throw new ItemNotFoundException("Comments not found matching the given criteria");

        var commentsDto = _commentMapper.MapCollection(comments);

        return new PaginatedResultDto<CommentResDto>(commentsDto, pageNumber, pageSize, totalPosts);
    }

    public async Task<ICollection<CommentResDto>> GetCommentsByPostIdAsync(int postId)
    {
        var post = await _postRepository.GetByIdAsync(postId);
        if (post == null) throw new ItemNotFoundException("No post found for the given id");
        
        var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);
        if (comments == null) throw new ItemNotFoundException("No comments found for the given post");
        var commentsDto = _commentMapper.MapCollection(comments);
        return commentsDto;
    }

    public async Task<CommentResDto?> CreateAsync(InternalCreateCommentData data)
    {
        var post = await _postRepository.GetByIdAsync(data.PostId);
        if (post == null) throw new ItemNotFoundException("No post found for the given id");
        
        var comment = _commentMapper.MapToModel(data);
        var createdComment = await _commentRepository.CreateAsync(comment);
        return _commentMapper.MapToResDto(createdComment);
    }

    public async Task<CommentResDto?> UpdateAsync(InternalUpdateCommentData data)
    {
        var existingComment = await _commentRepository.GetByIdAsync(data.CommentId);
        if (existingComment == null) throw new ItemNotFoundException();
        if (existingComment.UserId != data.CurrentUserId) throw new UserForbiddenException();

        existingComment.Content = data.Content;
        existingComment.UpdatedAt = DateTime.Now;

        var comment = await _commentRepository.UpdateAsync(existingComment);

        var updatedComment = _commentMapper.MapToResDto(comment);

        return updatedComment;
    }

    public async Task<CommentResDto?> DeleteAsync(InternalDeleteCommentData data)
    {
        var existingComment = await _commentRepository.GetByIdAsync(data.CommentId);

        if (existingComment == null) throw new ItemNotFoundException();
        if (existingComment.UserId != data.CurrentUserId) throw new UserForbiddenException();

        var deletedComment = await _commentRepository.DeleteAsync(existingComment);

        return _commentMapper.MapToResDto(deletedComment);
    }


    public async Task<CommentResDto?> GetByIdAsync(int id)
    {
        var comment = await _commentRepository.GetByIdAsync(id);
        if (comment == null) throw new ItemNotFoundException();
        return _commentMapper.MapToResDto(comment);
    }
}