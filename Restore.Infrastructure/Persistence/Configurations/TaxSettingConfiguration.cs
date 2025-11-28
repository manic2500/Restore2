namespace Restore.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restore.Domain.Entities;

public class TaxSettingConfiguration : IEntityTypeConfiguration<TaxSetting>
{
    public void Configure(EntityTypeBuilder<TaxSetting> builder)
    {
        //builder.ToTable("TaxSettings");

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Country)
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(x => x.State)
            .HasMaxLength(5);

        builder.Property(x => x.TaxPercentage)
            .HasPrecision(5, 2); // e.g. 8.25%

        builder.Property(x => x.UpdatedAt)
            .IsRequired();
    }
}
