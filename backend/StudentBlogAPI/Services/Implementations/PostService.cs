using StudentBlogAPI.Exceptions;
using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;
using StudentBlogAPI.Repository.Interfaces;
using StudentBlogAPI.Services.Interfaces;
using StudentBlogAPI.Utilities;

namespace StudentBlogAPI.Services.Implementations;

public class PostService : IPostService
{
    private readonly IPostMapper _postMapper;
    private readonly IPostRepository _postRepository;

    public PostService(IPostMapper postMapper, IPostRepository postRepository)
    {
        _postMapper = postMapper;
        _postRepository = postRepository;
    }

    public async Task<PaginatedResultDto<PostResDto>> GetAllAsync(InternalGetAllPostsData data)
    {
        var (posts, totalPosts) = await _postRepository.GetAllAsync(data);
        if (posts == null) throw new ItemNotFoundException("Posts not found matching the given criteria");
        var postsDto = _postMapper.MapCollection(posts);

        return new PaginatedResultDto<PostResDto>(postsDto, data.PageNumber, data.PageSize, totalPosts);
    }

    public async Task<PostResDto?> GetByIdAsync(InternalGetPostByIdData data)
    {
        var post = await _postRepository.GetByIdAsync(data.PostId);
        if (post == null) throw new ItemNotFoundException("Post not found");
        if (post.Status == PublicationStatus.Draft && post.UserId != data.CurrentUserId)
            throw new UserForbiddenException("Cannot access draft post of another user");
        return _postMapper.MapToResDto(post);
    }

    public async Task<PostResDto?> CreateAsync(InternalCreatePostData data)
    {
        var post = _postMapper.MapToModel(data);
        var createdPost = await _postRepository.CreateAsync(post);
        return _postMapper.MapToResDto(createdPost);
    }

    public async Task<PostResDto?> UpdateAsync(InternalUpdatePostData data)
    {
        var existingPost = await _postRepository.GetByIdAsync(data.PostId);

        if (existingPost == null) throw new ItemNotFoundException("Post not found");

        if (data.Status == PublicationStatus.Draft && existingPost.Status == PublicationStatus.Published)
            throw new UserForbiddenException("Cannot change status from published to draft");

        if (existingPost.UserId != data.CurrentUserId)
            throw new UserForbiddenException("You do not have permission to edit this post");

        existingPost.Title = data.Title;
        existingPost.Content = data.Content;
        existingPost.Status = data.Status;
        existingPost.UpdatedAt = DateTime.Now;

        var updatedPost = await _postRepository.UpdateAsync(existingPost);

        return _postMapper.MapToResDto(updatedPost);
    }

    public async Task<PostResDto?> DeleteAsync(InternalDeletePostData data)
    {
        var existingPost = await _postRepository.GetByIdAsync(data.PostId);

        if (existingPost == null) throw new ItemNotFoundException("Post not found");
        if (existingPost.UserId != data.UserId) throw new UserForbiddenException();

        var deletedPost = await _postRepository.DeleteAsync(existingPost);

        return _postMapper.MapToResDto(deletedPost);
    }
}