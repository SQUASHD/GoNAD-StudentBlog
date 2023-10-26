using StudentBlogAPI.Model.DTOs;

namespace StudentBlogAPI.Services.Interfaces;

public interface IUserService
{
    Task<ICollection<UserResDto>> GetAllAsync();
    Task<UserResDto?> GetByIdAsync(int id);
    Task<UserResDto?> UpdateAsync(int currentUserId, int userToUpdateId, UpdateUserInputReqDto inputReqDto);
    Task<UserResDto?> DeleteAsync(int currentUserId, int userToDeleteId);
}