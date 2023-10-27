using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentBlogAPI.Extensions;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Internal;
using StudentBlogAPI.Services.Interfaces;

namespace StudentBlogAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
public class PostsController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IJwtService _jwtService;
    private readonly IPostService _postService;

    public PostsController(IPostService postService, IJwtService jwtService, ICommentService commentService)
    {
        _postService = postService;
        _jwtService = jwtService;
        _commentService = commentService;
    }

    [HttpGet(Name = "GetPosts")]
    public async Task<ActionResult<PaginatedResultDto<PostResDto>>> GetPosts([FromQuery] int page = 1,
        [FromQuery] int size = 10)
    {
        if (page < 1 || size < 1) return BadRequest("Page and size parameters must be greater than 0");

        var paginatedPosts = await _postService.GetAllAsync(page, size);
        return Ok(paginatedPosts);
    }

    [HttpGet("{postId}", Name = "GetPostById")]
    public async Task<ActionResult<PostResDto>> GetPostById(int postId)
    {
        var post = await _postService.GetByIdAsync(postId);
        return Ok(post);
    }

    [HttpPost(Name = "CreatePost")]
    public async Task<ActionResult<PostResDto>> CreatePost(PostInputReqDto reqDto)
    {
        var currentUserId = this.GetCurrentUserId(_jwtService);

        var dto = new InternalCreatePostData(
            currentUserId,
            reqDto.Title,
            reqDto.Content
        );

        var createdPost = await _postService.CreateAsync(dto);

        return Ok(createdPost);
    }

    [HttpGet("{postId}/comments", Name = "GetCommentsForPost")]
    public async Task<ActionResult<CommentResDto>> GetCommentByPostId(int postId)
    {
        var comments = await _commentService.GetCommentsByPostIdAsync(postId);
        return Ok(comments);
    }

    [HttpPut("{postId}", Name = "UpdatePost")]
    public async Task<ActionResult<PostResDto>> UpdatePost(int postId, PostInputReqDto reqDto)
    {
        var currentUserId = this.GetCurrentUserId(_jwtService);

        var data = new InternalUpdatePostData(
            currentUserId,
            postId,
            reqDto.Title,
            reqDto.Content
        );

        var updatedPost = await _postService.UpdateAsync(data);

        return Ok(updatedPost);
    }

    [HttpDelete("{postID}", Name = "DeletePost")]
    public async Task<ActionResult<PostResDto>> DeletePost(int postId)
    {
        var currentUserId = this.GetCurrentUserId(_jwtService);

        var data = new InternalDeletePostData(
            currentUserId,
            postId
        );

        var deletedPost = await _postService.DeleteAsync(data);

        return Ok(deletedPost);
    }
}