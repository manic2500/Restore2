namespace Restore.Domain.Entities;

public class Voucher : BaseEntity
{
    public required string Code { get; set; } // unique coupon code
    public string? Description { get; set; }
    public bool IsActive { get; set; } = true;

    public DiscountType DiscountType { get; set; } = DiscountType.Flat;

    /// <summary>
    /// Discount in cents
    /// </summary>
    public long DiscountAmount { get; set; }

    /// <summary>
    /// For percentage vouchers, max discount allowed (in cents).
    /// </summary>
    public long? MaxDiscountAmount { get; set; }

    /// <summary>
    /// Minimum order amount in cents to apply this voucher
    /// </summary>
    public long MinOrderAmount { get; set; } = 0;

    /// <summary>
    /// Max number of times this voucher can be used
    /// </summary>
    public int? UsageLimit { get; set; }

    /// <summary>
    /// Number of times voucher has been used
    /// </summary>
    public int UsageCount { get; set; } = 0;

    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public DateTime EndDate { get; set; } = DateTime.UtcNow.AddMonths(1);
}


