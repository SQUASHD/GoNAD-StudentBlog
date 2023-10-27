using StudentBlogAPI.Exceptions;
using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Internal;
using StudentBlogAPI.Repository.Interfaces;
using StudentBlogAPI.Services.Interfaces;
using StudentBlogAPI.Utilities;

namespace StudentBlogAPI.Services;

public class PostService : IPostService
{
    private readonly IPostMapper _postMapper;
    private readonly IPostRepository _postRepository;

    public PostService(IPostMapper postMapper, IPostRepository postRepository)
    {
        _postMapper = postMapper;
        _postRepository = postRepository;
    }

    public async Task<PaginatedResultDto<PostResDto>> GetAllAsync(int pageNumber, int pageSize)
    {
        var (posts, totalPosts) = await _postRepository.GetAllAsync(pageNumber, pageSize);
        if (posts == null) throw new ItemNotFoundException("Posts not found matching the given criteria");
        var postsDto = _postMapper.MapCollection(posts);

        return new PaginatedResultDto<PostResDto>(postsDto, pageNumber, pageSize, totalPosts);
    }

    public async Task<PostResDto?> GetByIdAsync(int id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        if (post == null) throw new ItemNotFoundException("Post not found");
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
        if (existingPost.UserId != data.CurrenUserId) throw new UserForbiddenException();

        existingPost.Title = data.Title;
        existingPost.Content = data.Content;
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