using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentBlogAPI.Extensions;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Internal;
using StudentBlogAPI.Services.Interfaces;

namespace StudentBlogAPI.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PostsController : ControllerBase
{
    private readonly IJwtService _jwtService;
    private readonly IPostService _postService;

    public PostsController(IPostService postService, IJwtService jwtService)
    {
        _postService = postService;
        _jwtService = jwtService;
    }

    [HttpGet(Name = "GetPosts")]
    public async Task<ActionResult<ICollection<PostResDto>>> GetPosts()
    {
        var posts = await _postService.GetAllAsync();
        return posts.Any() ? Ok(posts) : NoContent();
    }

    [HttpGet("{postId}", Name = "GetPostById")]
    public async Task<ActionResult<PostResDto>> GetPostById(int postId)
    {
        var post = await _postService.GetByIdAsync(postId);
        return Ok(post);
    }

    [Authorize]
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

    [Authorize]
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

    [Authorize]
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