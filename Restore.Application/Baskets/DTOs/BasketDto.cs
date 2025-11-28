using Restore.Application.Products.DTOs;

namespace Restore.Application.Baskets.DTOs;

public record BasketDto(Guid BasketId, List<BasketItemDto> Items)
{
    /// <summary>
    /// All in cents
    /// </summary>
    public long SubTotal => Items.Sum(i => i.TotalPrice);

    public long Discount { get; set; }
    public long Shipping { get; set; }
    public long Tax { get; set; }

    public string? AppliedVoucher { get; set; }

    /// <summary>
    /// Grand total in cents
    /// </summary>
    public long GrandTotal => SubTotal + Tax + Shipping - Discount;
}

public record BasketItemDto(
        Guid ItemId,
        Guid ProductId,
        string Name,
        long Price, // cents
        string PictureUrl,
        string Brand,
        string Type,
        int Quantity
    )
{
    public long TotalPrice => Price * Quantity;

}

