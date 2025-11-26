using Restore.Application.Products.DTOs;

namespace Restore.Application.Baskets.DTOs;

public record BasketDto(Guid BasketId, List<BasketItemDto> Items)
{
    // Optional: total amount for the basket
    public decimal TotalAmount => Items.Sum(i => i.TotalPrice);
}

public record BasketItemDto(
        Guid ProductId,
        string Name,
        long Price,
        string PictureUrl,
        string Brand,
        string Type,
        int Quantity
    )
{
    public decimal TotalPrice => Price * Quantity;

}

