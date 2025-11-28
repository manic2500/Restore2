using System;

namespace Restore.Domain.Entities;

// Order.cs
public class Order : BaseEntity
{
    public Guid UserId { get; set; }

    public long SubTotal { get; set; }        // in cents
    public long Tax { get; set; }             // in cents
    public long Shipping { get; set; }        // in cents
    public long Discount { get; set; }        // in cents
    public long Total { get; set; }           // SubTotal + Tax + Shipping - Discount

    public string? AppliedVoucher { get; set; }

    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<OrderItem> Items { get; set; } = [];
}

