namespace Restore.Common.Interfaces;

public interface IUnitOfWork : IDisposable
{
    /*     IBlogPostRepository BlogPosts { get; }
        ICategoryRepository Categories { get; }
     */
    Task<int> SaveChangesAsync();

    // 🔹 Transaction control
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();

    Task EnlistContextAsync(object newContext);
}

