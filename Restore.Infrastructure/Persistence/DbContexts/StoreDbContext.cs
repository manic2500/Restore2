using Microsoft.EntityFrameworkCore;
using Restore.Domain.Entities;
using Restore.Infrastructure.Persistence.Extensions;


namespace Restore.Infrastructure.Persistence.DbContexts;

public class StoreDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplySoftDeleteFilter();
        /* modelBuilder.Entity<BaseEntity>(entity =>
        {
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.PublicId).HasDefaultValueSql("gen_random_uuid()");
        }); */
    }
}



// dotnet ef migrations add InitialCreate -s Restore.API -p Restore.Infrastructure  -o Persistence/Migrations/Store -c "StoreDbContext"
// dotnet ef migrations remove -s Restore.API -p Restore.Infrastructure -c "StoreDbContext"

// dotnet ef database update -s Restore.API  -p Restore.Infrastructure -c "StoreDbContext"
// dotnet ef database drop -s Restore.API -p Restore.Infrastructure -c "StoreDbContext"

// dotnet ef migrations add TEST -s Restore.API -p Restore.Infrastructure -c "StoreDbContext"
