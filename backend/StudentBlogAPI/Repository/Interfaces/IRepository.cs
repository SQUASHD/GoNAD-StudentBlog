namespace StudentBlogAPI.Repository.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<ICollection<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(int id);
    Task<TEntity> CreateAsync(TEntity entity);
    Task<TEntity?> UpdateAsync(int id, TEntity entity);
    Task<TEntity?> DeleteAsync(int id);
}