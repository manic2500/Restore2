using Microsoft.EntityFrameworkCore;
using Restore.Domain.Entities;

namespace Restore.Infrastructure.Persistence.Extensions;

public static class DbContextExtensions
{
    public static Task<T?> FindByXidAsync<T>(
        this DbContext context, Guid publicId)
        where T : BaseEntity
    {
        return context.Set<T>().FirstOrDefaultAsync(x => x.ExtId == publicId);
    }
}

// var item = await _db.FindByPublicIdAsync<BasketItem>(publicItemId);
