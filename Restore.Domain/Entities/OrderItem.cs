namespace Restore.Domain.Entities;

// OrderItem.cs
public class OrderItem : BaseEntity
{
    public Guid ProductXid { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public long Price { get; set; }       // unit price in cents
    public long Total => Price * Quantity;

    // Navigation property
    public required Order Order { get; set; }       // EF Core relationship
    public int OrderId { get; set; }
}

