namespace Restore.Infrastructure.Persistence.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Restore.Domain.Entities;

public class DeliverySettingConfiguration : IEntityTypeConfiguration<DeliverySetting>
{
    public void Configure(EntityTypeBuilder<DeliverySetting> builder)
    {
        //builder.ToTable("DeliverySettings");

        builder.Property(x => x.Currency)
            .IsRequired()
            .HasMaxLength(5);

        builder.Property(x => x.FlatFee)
            .IsRequired();

        builder.Property(x => x.FreeShippingThreshold)
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .IsRequired();
    }
}
