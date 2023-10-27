using StudentBlogAPI.Exceptions;
using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;
using StudentBlogAPI.Repository.Interfaces;
using StudentBlogAPI.Services.Interfaces;

namespace StudentBlogAPI.Services;

public class PostService : IPostService
{
    private readonly IPostMapper _postMapper;
    private readonly IRepository<Post> _postRepository;

    public PostService(IPostMapper postMapper, IRepository<Post> postRepository)
    {
        _postMapper = postMapper;
        _postRepository = postRepository;
    }

    public async Task<ICollection<PostResDto>> GetAllAsync()
    {
        var posts = await _postRepository.GetAllAsync();
        return _postMapper.MapCollection(posts);
    }

    public async Task<PostResDto?> GetByIdAsync(int id)
    {
        var post = await _postRepository.GetByIdAsync(id);
        return post != null ? _postMapper.MapToResDto(post) : null;
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