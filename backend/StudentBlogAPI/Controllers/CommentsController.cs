using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentBlogAPI.Extensions;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Internal;
using StudentBlogAPI.Services.Interfaces;

namespace StudentBlogAPI.Controllers;

[ApiController]
[Route("api/v1/")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IJwtService _jwtService;

    public CommentsController(ICommentService commentService, IJwtService jwtService)
    {
        _commentService = commentService;
        _jwtService = jwtService;
    }

    [HttpGet("posts/{postId}/comments")]
    public async Task<ActionResult<CommentResDto>> GetCommentById(int id)
    {
        var comment = await _commentService.GetByIdAsync(id);
        return comment != null ? Ok(comment) : NotFound();
    }

    [HttpPost("posts/{postId}/comments", Name = "CreateComment")]
    public async Task<ActionResult<CommentResDto>> CreateComment(int postId, CommentInputReqDto inputReqDto)
    {
        var currentUserId = this.GetCurrentUserId(_jwtService);

        var dto = new InternalCreateCommentData(
            postId,
            currentUserId,
            inputReqDto.Content
        );
        var newComment = await _commentService.CreateAsync(dto);
        return Ok(newComment);
    }

    [Authorize]
    [HttpPut("comments/{commentId}", Name = "UpdateComment")]
    public async Task<ActionResult<CommentResDto>> UpdateComment(int commentId, CommentInputReqDto inputReqDto)
    {
        var currentUserId = this.GetCurrentUserId(_jwtService);

        var dto = new InternalUpdateCommentData(
            commentId,
            currentUserId,
            inputReqDto.Content
        );

        var updatedComment = await _commentService.UpdateAsync(currentUserId, commentId, dto);
        return Ok(updatedComment);
    }

    [Authorize]
    [HttpDelete("comments/{commentId}", Name = "DeleteComment")]
    public async Task<ActionResult<CommentResDto>> DeleteComment(int id)
    {
        var currentUserId = this.GetCurrentUserId(_jwtService);
        ;
        var deletedComment = await _commentService.DeleteAsync(currentUserId, id);
        return Ok(deletedComment);
    }
}