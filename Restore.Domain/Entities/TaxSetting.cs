namespace Restore.Domain.Entities;

public class TaxSetting : BaseEntity
{
    /// <summary>
    /// Tax percentage (e.g., 8.0 means 8%)
    /// </summary>
    public decimal TaxPercentage { get; set; }

    /// <summary>
    /// Tax Name (GST, VAT, Sales Tax)
    /// </summary>
    public string Name { get; set; } = "GST";

    /// <summary>
    /// Country code (optional, for future multi-region support)
    /// </summary>
    public string Country { get; set; } = "IN";

    /// <summary>
    /// State code (optional)
    /// </summary>
    public string? State { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
