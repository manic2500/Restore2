namespace Restore.Common.Interfaces;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);
    Task DeleteAsync(Guid xid);
    Task<TEntity[]> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllDeletedAsync();
    Task<TEntity?> GetByXidAsync(Guid xid, bool includeDeleted = false);

    /* 
        SaveChanges() in a generic repository violates the UoW pattern. 
    */
    //Task<int> SaveChangesAsync();

    /* 
        No need to call Update() — in fact, Update() can be harmful, 
        because it marks all properties as modified, 
        even if only one property changed.
     */
    //Task UpdateAsync(TEntity entity);
}