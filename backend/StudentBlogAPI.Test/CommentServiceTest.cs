using Moq;
using StudentBlogAPI.Exceptions;
using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Entities;
using StudentBlogAPI.Model.Internal;
using StudentBlogAPI.Repository.Interfaces;
using StudentBlogAPI.Services;
using StudentBlogAPI.Utilities;

namespace StudentBlogAPI.Test;
public class CommentServiceTests
{
    private readonly Mock<ICommentMapper> _commentMapperMock;
    private readonly Mock<ICommentRepository> _commentRepositoryMock;
    private readonly Mock<IPostRepository> _postRepositoryMock;
    private readonly CommentService _commentService;

    public CommentServiceTests()
    {
        _commentMapperMock = new Mock<ICommentMapper>();
        _commentRepositoryMock = new Mock<ICommentRepository>();
        _postRepositoryMock = new Mock<IPostRepository>();
        _commentService = new CommentService(_commentMapperMock.Object, _commentRepositoryMock.Object, _postRepositoryMock.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsPaginatedResultDto_WhenCommentsExist()
    {
        // Arrange
        var currentDate = DateTime.Now;
        const int pageNumber = 1;
        const int pageSize = 10;
        var comments = new List<Comment>
            {
                new() { Id = 1, Content = "Comment 1", PostId = 1 , CreatedAt = currentDate, UpdatedAt = currentDate},
                new() { Id = 2, Content = "Comment 2" , PostId = 2, CreatedAt = currentDate, UpdatedAt = currentDate}
            };
        const int totalPosts = 2;
        var commentsDto = new List<CommentResDto>
            {
                new(1, 2,1,"Comment 1", currentDate, currentDate ),
                new(2, 1, 1,"Comment 2", currentDate, currentDate)
            };
        _commentRepositoryMock.Setup(x => x.GetAllAsync(pageNumber, pageSize)).ReturnsAsync((comments, totalPosts));
        _commentMapperMock.Setup(x => x.MapCollection(comments)).Returns(commentsDto);

        // Act
        var result = await _commentService.GetAllAsync(pageNumber, pageSize);

        // Assert
        Assert.IsType<PaginatedResultDto<CommentResDto>>(result);
        Assert.Equal(commentsDto, result.Items);
        Assert.Equal(pageNumber, result.CurrentPage);
        Assert.Equal(pageSize, result.PageSize);
        Assert.Equal(totalPosts, result.TotalItems);
    }

    [Fact]
    public async Task GetAllAsync_ThrowsItemNotFoundException_WhenCommentsDoNotExist()
    {
        // Arrange
        const int pageNumber = 1;
        const int pageSize = 10;
        List<Comment>? comments = null;
        const int totalPosts = 0;
        _commentRepositoryMock.Setup(x => x.GetAllAsync(pageNumber, pageSize)).ReturnsAsync((comments, totalPosts));

        // Act
        await Assert.ThrowsAsync<ItemNotFoundException>(() => _commentService.GetAllAsync(pageNumber, pageSize));
    }

    [Fact]
    public async Task GetCommentsByPostIdAsync_ReturnsCommentResDtoCollection_WhenCommentsExist()
    {
        // Arrange
        const int postId = 1;
        var createdDate = DateTime.Now;
        var comments = new List<Comment>
            {
                new() { Id = 1, Content = "Comment 1" , CreatedAt = createdDate, PostId = 1, UserId = 1 , UpdatedAt = createdDate},
                new() { Id = 2, Content = "Comment 2", CreatedAt = createdDate , PostId = 1 , UserId = 2, UpdatedAt = createdDate}
            };
        var commentsDto = new List<CommentResDto>
            {
                new CommentResDto(1, postId, 1, "Comment 1", createdDate, createdDate),
                new CommentResDto(2, postId, 2, "Comment 2", createdDate, createdDate )
            };
        _commentRepositoryMock.Setup(x => x.GetCommentsByPostIdAsync(postId)).ReturnsAsync(comments);
        _commentMapperMock.Setup(x => x.MapCollection(comments)).Returns(commentsDto);
        _postRepositoryMock.Setup(x => x.GetByIdAsync(postId)).ReturnsAsync(new Post { Id = postId });

        // Act
        var result = await _commentService.GetCommentsByPostIdAsync(postId);

        // Assert
        Assert.Equal(commentsDto, result);
    }

    [Fact]
    public async Task CreateAsync_ThrowsIfPostDoesNotExist()
    {
        // Arrange
        const int postId = 1;
        var comment = new Comment { Id = 1, Content = "Comment 1", PostId = 1};
        
        _postRepositoryMock.Setup(x => x.GetByIdAsync(postId)).ReturnsAsync((Post?)null);
        
        // Act & Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(() => _commentService.CreateAsync(new InternalCreateCommentData(1, postId, "Comment 1")));
    }

    [Fact]
    public async Task GetCommentsByPostIdAsync_ThrowsItemNotFoundException_WhenCommentsDoNotExist()
    {
        // Arrange
        var postId = 1;
        List<Comment>? comments = null;
        _commentRepositoryMock.Setup(x => x.GetCommentsByPostIdAsync(postId)).ReturnsAsync(comments);

        // Act & Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(() => _commentService.GetCommentsByPostIdAsync(postId));
    }

    [Fact]
    public async Task CreateAsync_ReturnsCommentResDto_WhenCommentIsCreated()
    {
        // Arrange
        var data = new InternalCreateCommentData(1, 1, "Comment 1");
        var comment = new Comment { Id = 1, Content = "Comment 1" };
        var createdComment = new Comment { Id = 1, Content = "Comment 1" };
        var commentDto = new CommentResDto(createdComment.Id, createdComment.PostId, createdComment.UserId, createdComment.Content, createdComment.CreatedAt, createdComment.UpdatedAt);
        _commentMapperMock.Setup(x => x.MapToModel(data)).Returns(comment);
        _commentRepositoryMock.Setup(x => x.CreateAsync(comment)).ReturnsAsync(createdComment);
        _commentMapperMock.Setup(x => x.MapToResDto(createdComment)).Returns(commentDto);

        // Act
        var result = await _commentService.CreateAsync(data);

        // Assert
        Assert.Equal(commentDto, result);
    }

    [Fact]
    public async Task UpdateAsync_ReturnsCommentResDto_WhenCommentIsUpdated()
    {
        // Arrange
        var data = new InternalUpdateCommentData(1, 1, "Updated comment");
        var existingComment = new Comment { Id = 1, Content = "Comment 1", UserId = 1 };
        var updatedComment = new Comment { Id = 1, Content = "Updated comment", UserId = 1 };
        var commentDto = new CommentResDto(updatedComment.Id, updatedComment.PostId, updatedComment.UserId, "Updated comment", updatedComment.CreatedAt, updatedComment.UpdatedAt);
        _commentRepositoryMock.Setup(x => x.GetByIdAsync(data.CommentId)).ReturnsAsync(existingComment);
        _commentRepositoryMock.Setup(x => x.UpdateAsync(existingComment)).ReturnsAsync(updatedComment);
        _commentMapperMock.Setup(x => x.MapToResDto(updatedComment)).Returns(commentDto);

        // Act
        var result = await _commentService.UpdateAsync(data);

        // Assert
        Assert.Equal(commentDto, result);
    }

    [Fact]
    public async Task UpdateAsync_ThrowsItemNotFoundException_WhenCommentDoesNotExist()
    {
        // Arrange
        var data = new InternalUpdateCommentData(1, 1, "Updated comment");
        Comment? existingComment = null;
        _commentRepositoryMock.Setup(x => x.GetByIdAsync(data.CommentId)).ReturnsAsync(existingComment);

        // Act & Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(() => _commentService.UpdateAsync(data));
    }



    [Fact]
    public async Task UpdateAsync_ThrowsUserForbiddenException_WhenUserIsNotAuthorizedToUpdateComment()
    {
        // Arrange
        var data = new InternalUpdateCommentData(2, 2, "Updated comment");
        var existingComment = new Comment { Id = 1, Content = "Comment 1", UserId = 1 };
        _commentRepositoryMock.Setup(x => x.GetByIdAsync(data.CommentId)).ReturnsAsync(existingComment);

        // Act & Assert
        await Assert.ThrowsAsync<UserForbiddenException>(() => _commentService.UpdateAsync(data));
    }

    [Fact]
    public async Task DeleteAsync_ReturnsCommentResDto_WhenCommentIsDeleted()
    {
        // Arrange
        var data = new InternalDeleteCommentData(1, 1);
        var existingComment = new Comment { Id = 1, Content = "Comment 1", UserId = 1 };
        var deletedComment = new Comment { Id = 1, Content = "Comment 1", UserId = 1 };
        var commentDto = new CommentResDto(deletedComment.Id, deletedComment.PostId, deletedComment.UserId, deletedComment.Content, deletedComment.CreatedAt, deletedComment.UpdatedAt);
        _commentRepositoryMock.Setup(x => x.GetByIdAsync(data.CommentId)).ReturnsAsync(existingComment);
        _commentRepositoryMock.Setup(x => x.DeleteAsync(existingComment)).ReturnsAsync(deletedComment);
        _commentMapperMock.Setup(x => x.MapToResDto(deletedComment)).Returns(commentDto);

        // Act
        var result = await _commentService.DeleteAsync(data);

        // Assert
        Assert.Equal(commentDto, result);
    }

    [Fact]
    public async Task DeleteAsync_ThrowsItemNotFoundException_WhenCommentDoesNotExist()
    {
        // Arrange
        var data = new InternalDeleteCommentData(1, 1);
        Comment? existingComment = null;
        _commentRepositoryMock.Setup(x => x.GetByIdAsync(data.CommentId)).ReturnsAsync(existingComment);

        // Act & Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(() => _commentService.DeleteAsync(data));
    }

    [Fact]
    public async Task DeleteAsync_ThrowsUserForbiddenException_WhenUserIsNotAuthorizedToDeleteComment()
    {
        // Arrange
        var data = new InternalDeleteCommentData(2, 2);
        var existingComment = new Comment { Id = 1, Content = "Comment 1", UserId = 1 };
        _commentRepositoryMock.Setup(x => x.GetByIdAsync(data.CommentId)).ReturnsAsync(existingComment);

        // Act & Assert
        await Assert.ThrowsAsync<UserForbiddenException>(() => _commentService.DeleteAsync(data));
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCommentResDto_WhenCommentExists()
    {
        // Arrange
        var id = 1;
        var comment = new Comment { Id = 1, Content = "Comment 1" };
        var commentDto = new CommentResDto(comment.Id, comment.PostId, comment.UserId, comment.Content, comment.CreatedAt, comment.UpdatedAt);
        _commentRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(comment);
        _commentMapperMock.Setup(x => x.MapToResDto(comment)).Returns(commentDto);

        // Act
        var result = await _commentService.GetByIdAsync(id);

        // Assert
        Assert.Equal(commentDto, result);
    }

    [Fact]
    public async Task GetByIdAsync_ThrowsItemNotFoundException_WhenCommentDoesNotExist()
    {
        // Arrange
        var id = 1;
        Comment? comment = null;
        _commentRepositoryMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(comment);

        // Act & Assert
        await Assert.ThrowsAsync<ItemNotFoundException>(() => _commentService.GetByIdAsync(id));
    }
}
