using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Internal;

namespace StudentBlogAPI.Services.Interfaces;

public interface IUserService
{
    Task<PaginatedResultDto<UserResDto>> GetAllAsync(int pageNumber, int pageSize);
    Task<UserResDto?> GetByIdAsync(int id);
    Task<ICollection<PostResDto>> GetPostsByUserIdAsync(int userId);
    Task<UserResDto?> UpdateUserInfoAsync(InternalUpdateUserInfoData data);
    Task<UserResDto?> UpdatePasswordAsync(InternalUpdatePasswordReqData data);

    Task<UserResDto?> DeleteAsync(InternalDeleteUserData data);
}