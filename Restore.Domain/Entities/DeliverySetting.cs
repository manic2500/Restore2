namespace Restore.Domain.Entities;

public class DeliverySetting : BaseEntity
{
    /// <summary>
    /// Flat delivery fee in smallest currency unit (e.g., paise)
    /// </summary>
    public long FlatFee { get; set; }

    /// <summary>
    /// Minimum order amount for free shipping (in smallest currency unit)
    /// </summary>
    public long FreeShippingThreshold { get; set; }

    public string Currency { get; set; } = "INR";

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
