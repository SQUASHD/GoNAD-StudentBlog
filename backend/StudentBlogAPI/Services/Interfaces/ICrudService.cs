namespace StudentBlogAPI.Services.Interfaces;

public interface ICrudService<TDto>
{
    Task<ICollection<TDto>> GetAllAsync();
    Task<TDto?> GetByIdAsync(int id);
    Task<TDto?> CreateAsync(TDto dto);
    Task<TDto?> UpdateAsync(int id, TDto dto);
    Task<TDto?> DeleteAsync(int id);
}