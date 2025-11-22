using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Restore.Infrastructure.Persistence.UnitOfWork;

public class UnitOfWork(IEnumerable<DbContext> dbContexts) : IUnitOfWork, IDisposable
{
    private IDbContextTransaction? transaction;

    public async Task BeginTransactionAsync()
    {
        if (transaction != null)
            return;

        // Pick the first context as the "primary"
        var primaryContext = dbContexts.First();
        transaction = await primaryContext.Database.BeginTransactionAsync();
        // All others share the same transaction
        foreach (var context in dbContexts.Skip(1))
        {
            await context.Database.UseTransactionAsync(
                primaryContext.Database.CurrentTransaction!.GetDbTransaction());
        }
    }

    public async Task<int> SaveChangesAsync()
    {
        int total = 0;
        foreach (var context in dbContexts)
        {
            total += await context.SaveChangesAsync();
        }
        return total;
    }

    public async Task CommitTransactionAsync()
    {
        if (transaction == null) return;
        await transaction.CommitAsync();
        await DisposeTransactionAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        if (transaction == null) return;
        await transaction.RollbackAsync();
        await DisposeTransactionAsync();
    }

    private async Task DisposeTransactionAsync()
    {
        await transaction!.DisposeAsync();
        transaction = null;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        foreach (var context in dbContexts)
            context.Dispose();
        transaction?.Dispose();
    }

    public async Task EnlistContextAsync(object newContext)
    {
        if (newContext is DbContext dbContext && transaction != null)
        {
            await dbContext.Database.UseTransactionAsync(
                dbContexts.First().Database.CurrentTransaction!.GetDbTransaction());
        }
    }

}

// /* 
// // ****** To use the EnlistContext for LAZY Init of context ******
// await unitOfWork.BeginTransactionAsync();
// await unitOfWork.EnlistContextAsync(billingDbContext);
//  */

// /* 
// // ****** Register all contexts in DI ******
// builder.Services.AddScoped<IUnitOfWork>(sp =>
// {
//     var contexts = new List<DbContext>
//     {
//         sp.GetRequiredService<AppDbContext>(),
//         sp.GetRequiredService<IdentityDbContext>(),
//         sp.GetRequiredService<AuditDbContext>()
//     };

//     return new UnitOfWork(contexts);
// });
//  */

// // ****** OLD Unit of Work with single context ******
// /* 
// public class UnitOfWork(
//     AppDbContext context,
//     ICategoryRepository categoryRepository,
//     IBlogPostRepository blogPostRepository) : IUnitOfWork
// {
//     private IDbContextTransaction? transaction;

//     public IBlogPostRepository BlogPosts { get; } = blogPostRepository;
//     public ICategoryRepository Categories { get; } = categoryRepository;

//     public async Task<int> SaveChangesAsync()
//     {
//         return await context.SaveChangesAsync();
//     }

//     // 🔹 Transaction control
//     public async Task BeginTransactionAsync()
//     {
//         transaction ??= await context.Database.BeginTransactionAsync();
//     }

//     public async Task CommitTransactionAsync()
//     {
//         if (transaction != null)
//         {
//             await transaction.CommitAsync();
//             await transaction.DisposeAsync();
//             transaction = null;
//         }
//     }

//     public async Task RollbackTransactionAsync()
//     {
//         if (transaction != null)
//         {
//             await transaction.RollbackAsync();
//             await transaction.DisposeAsync();
//             transaction = null;
//         }
//     }
//     public void Dispose()
//     {
//         GC.SuppressFinalize(this);
//         transaction?.Dispose();
//         context.Dispose();
//     }
// }
//  */

// /* 

// Scenario	What happens	Is atomic?	Do you need explicit transaction?

// One SaveChanges() for both entities	EF’s implicit transaction covers both (atomic)

// Two separate SaveChanges() calls	Each call commits independently	(explicit)

// Multiple repositories / mixed operations	Some EF, some not	(explicit)
//  */