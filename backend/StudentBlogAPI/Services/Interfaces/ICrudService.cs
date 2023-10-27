namespace StudentBlogAPI.Services.Interfaces;

public interface ICrudService<TDto, TCreateData, TUpdateData, TDeleteData>
{
    Task<ICollection<TDto>> GetAllAsync();
    Task<TDto?> GetByIdAsync(int id);
    Task<TDto?> CreateAsync(TCreateData data);
    Task<TDto?> UpdateAsync(TUpdateData data);
    Task<TDto?> DeleteAsync(TDeleteData data);
}