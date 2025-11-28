namespace Restore.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restore.Domain.Entities;

public class VoucherConfiguration : IEntityTypeConfiguration<Voucher>
{
    public void Configure(EntityTypeBuilder<Voucher> builder)
    {
        //builder.ToTable("Vouchers");

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.Property(x => x.DiscountAmount)
            .HasPrecision(10, 2)
            .IsRequired();

        builder.Property(x => x.MaxDiscountAmount)
            .HasPrecision(10, 2);

        builder.Property(x => x.MinOrderAmount)
            .HasPrecision(10, 2);

        builder.Property(x => x.StartDate)
            .IsRequired();

        builder.Property(x => x.EndDate)
            .IsRequired();

        builder.Property(x => x.IsActive)
            .HasDefaultValue(true);

        // Partial unique index for PostgreSQL
        builder
        .HasIndex(v => v.Code)
        .IsUnique()
        .HasFilter("\"is_deleted\" = FALSE"); // PostgreSQL requires double quotes for column names
    }
}
