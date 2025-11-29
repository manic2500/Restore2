using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Restore.Common.Interfaces;
using Restore.Domain.Entities;


namespace Restore.Infrastructure.Persistence.Repositories;


public class GenericRepository<TEntity, TContext> : IGenericRepository<TEntity>
where TEntity : BaseEntity
where TContext : DbContext
{
    protected readonly TContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    public GenericRepository(TContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual async Task<TEntity[]> GetAllAsync()
    {
        return [.. await _dbSet.ToListAsync()];
    }

    public virtual async Task<TEntity?> GetByExIdAsync(Guid exId, bool includeDeleted = false)
    {
        var query = _dbSet.AsQueryable();
        if (includeDeleted)
            query = query.IgnoreQueryFilters();

        return await query.FirstOrDefaultAsync(x => x.ExtId == exId);
        //return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }


    // 🔹 Soft delete
    public virtual async Task DeleteAsync(Guid exId)
    {
        var entity = await GetByExIdAsync(exId);
        entity?.IsDeleted = true;
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllDeletedAsync()
    {
        return await _dbSet
                    .IgnoreQueryFilters()
                    .Where(c => c.IsDeleted)
                    .ToListAsync();
    }

    public async Task<TEntity> GetSingleOrThrowAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        string? errorMessage = null)
    {
        var query = predicate == null
            ? _dbSet
            : _dbSet.Where(predicate);

        return await query.FirstOrDefaultAsync()
               ?? throw new KeyNotFoundException(errorMessage ?? $"{typeof(TEntity).Name} not found.");
    }

    public async Task<TEntity?> GetSingleAsync(Expression<Func<TEntity, bool>>? predicate = null)
    {
        var query = predicate == null
            ? _dbSet
            : _dbSet.Where(predicate);

        return await query.FirstOrDefaultAsync();

    }
    /* public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    } */

    /* 
        No need to call Update() — in fact, Update() can be harmful, 
        because it marks all properties as modified, 
        even if only one property changed.
     */
    /* public virtual Task UpdateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return Task.CompletedTask;
    } */

}
