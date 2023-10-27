using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentBlogAPI.Extensions;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Internal;
using StudentBlogAPI.Services.Interfaces;
using StudentBlogAPI.Utilities;

namespace StudentBlogAPI.Controllers;


[ApiController]
[Route("api/v1/[controller]")]
public class CommentsController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IJwtService _jwtService;

    public CommentsController(ICommentService commentService, IJwtService jwtService)
    {
        _commentService = commentService;
        _jwtService = jwtService;
    }

    [HttpGet(Name = "GetComments")]
    public async Task<ActionResult<PaginatedResultDto<PostResDto>>> GetComments([FromQuery] int page = 1,
        [FromQuery] int size = 10)
    {
        if (page < 1 || size < 1) return BadRequest("Page and size parameters must be greater than 0");

        var paginatedComments = await _commentService.GetAllAsync(page, size);
        return Ok(paginatedComments);
    }

    [HttpPost("{postId}", Name = "CreateComment")]
    public async Task<ActionResult<CommentResDto>> CreateComment(int postId, CommentInputReqDto inputReqDto)
    {
        var currentUserId = this.GetCurrentUserId(_jwtService);

        var dto = new InternalCreateCommentData(
            currentUserId,
            postId,
            inputReqDto.Content
        );
        var newComment = await _commentService.CreateAsync(dto);
        return Ok(newComment);
    }

    [HttpPut("{commentId}", Name = "UpdateComment")]
    public async Task<ActionResult<CommentResDto>> UpdateComment(int commentId, CommentInputReqDto inputReqDto)
    {
        var currentUserId = this.GetCurrentUserId(_jwtService);

        var data = new InternalUpdateCommentData(
            currentUserId,
            commentId,
            inputReqDto.Content
        );

        var updatedComment = await _commentService.UpdateAsync(data);
        return Ok(updatedComment);
    }

    [HttpDelete("{commentId}", Name = "DeleteComment")]
    public async Task<ActionResult<CommentResDto>> DeleteComment(int commentId)
    {
        var currentUserId = this.GetCurrentUserId(_jwtService);

        var data = new InternalDeleteCommentData(
            currentUserId,
            commentId
        );

        var deletedComment = await _commentService.DeleteAsync(data);
        return Ok(deletedComment);
    }
}