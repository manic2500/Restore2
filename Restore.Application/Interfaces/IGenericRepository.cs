using Restore.Domain.Entities;

namespace Restore.Application.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> AddAsync(TEntity entity);
    Task DeleteAsync(Guid id);
    Task<TEntity[]> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllDeletedAsync();
    Task<TEntity?> GetByPublicIdAsync(Guid id, bool includeDeleted = false);
    Task<int> SaveChangesAsync();
    Task UpdateAsync(TEntity entity);
}