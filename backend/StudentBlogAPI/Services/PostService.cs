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
        return post != null ? _postMapper.MapToDto(post) : null;
    }

    public async Task<PostResDto?> CreateAsync(int userId, InternalCreatePostData data)
    {
        var post = _postMapper.MapCreateToModel(data);
        var createdPost = await _postRepository.CreateAsync(post);
        return _postMapper.MapToDto(createdPost);
    }

    public async Task<PostResDto?> UpdateAsync(int userId, int id, UpdatePostDto dto)
    {
        var existingPost = await _postRepository.GetByIdAsync(id);
        if (existingPost == null) throw new ItemNotFoundException("Post not found");
        if (existingPost.UserId != userId) throw new UserForbiddenException();

        var post = _postMapper.MapUpdateToModel(dto);
        post.UserId = userId;
        var updatedPost = await _postRepository.UpdateAsync(id, post);
        return updatedPost != null ? _postMapper.MapToDto(updatedPost) : null;
    }

    public async Task<PostResDto?> DeleteAsync(int userId, int id)
    {
        var existingPost = await _postRepository.GetByIdAsync(id);
        if (existingPost == null) throw new ItemNotFoundException("Post not found");
        if (existingPost.UserId != userId) throw new UserForbiddenException();
        var deletedPost = await _postRepository.DeleteAsync(id);
        return deletedPost != null ? _postMapper.MapToDto(deletedPost) : null;
    }
}