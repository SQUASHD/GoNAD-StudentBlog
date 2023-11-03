using StudentBlogAPI.Model.DTOs;
using StudentBlogAPI.Model.Internal;
using StudentBlogAPI.Utilities;

namespace StudentBlogAPI.Services.Interfaces;

public interface IUserService
{
    Task<PaginatedResultDto<UserResDto>> GetAllAsync(int pageNumber, int pageSize);
    Task<UserResDto?> GetByIdAsync(int id);
    Task<PaginatedResultDto<PostResDto>> GetPostsByUserIdAsync(InternalGetPostsByUserIdData data);
    Task<UserResDto?> UpdateUserInfoAsync(InternalUpdateUserInfoData data);
    Task<UserResDto?> UpdatePasswordAsync(InternalUpdatePasswordReqData data);

    Task<UserResDto?> DeleteAsync(InternalDeleteUserData data);
}