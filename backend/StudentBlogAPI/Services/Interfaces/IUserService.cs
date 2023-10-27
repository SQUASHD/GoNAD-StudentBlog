using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Services.Interfaces;

public interface IUserService
{
    Task<ICollection<UserResDto>> GetAllAsync();
    Task<UserResDto?> GetByIdAsync(int id);
    Task<UserResDto?> UpdateUserInfoAsync(InternalUpdateUserInfoData data);
    Task<UserResDto?> UpdatePasswordAsync(InternalUpdatePasswordReqData data);

    Task<UserResDto?> DeleteAsync(InternalDeleteUserData data);
}