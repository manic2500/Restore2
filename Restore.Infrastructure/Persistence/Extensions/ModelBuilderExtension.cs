using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Restore.Domain.Entities;

namespace Restore.Infrastructure.Persistence.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplySoftDeleteFilter(this ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;

            if (typeof(BaseEntity).IsAssignableFrom(clrType))
            {
                var method = typeof(ModelBuilderExtensions)
                    .GetMethod(nameof(SetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)!
                    .MakeGenericMethod(clrType);

                method.Invoke(null, [builder]);
            }
        }
    }

    private static void SetSoftDeleteFilter<TEntity>(ModelBuilder modelBuilder)
        where TEntity : BaseEntity
    {
        modelBuilder.Entity<TEntity>().HasQueryFilter(e => !e.IsDeleted);
    }
}
