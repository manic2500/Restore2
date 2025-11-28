using Microsoft.EntityFrameworkCore;
using Restore.Domain.Entities;
using Restore.Infrastructure.Persistence.Extensions;


namespace Restore.Infrastructure.Persistence.DbContexts;

public class StoreDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Basket> Baskets { get; set; }
    public DbSet<BasketItem> BasketItems { get; set; }

    public DbSet<TaxSetting> TaxSettings => Set<TaxSetting>();
    public DbSet<DeliverySetting> DeliverySettings => Set<DeliverySetting>();
    public DbSet<Voucher> Vouchers => Set<Voucher>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplySoftDeleteFilter();

        builder.Entity<Basket>()
           .HasMany(b => b.Items)
           .WithOne()
           .HasForeignKey(i => i.BasketId)
           .OnDelete(DeleteBehavior.Cascade);


        builder.ApplyConfigurationsFromAssembly(typeof(StoreDbContext).Assembly);

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

// dotnet ef migrations add AddUniqueIndexVoucher -s Restore.API -p Restore.Infrastructure -c "StoreDbContext"
// dotnet ef migrations add AddTaxDeliveryVoucher -s Restore.API -p Restore.Infrastructure -c "StoreDbContext"