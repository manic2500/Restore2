using Restore.Application.Baskets.DTOs;
using Restore.Common.DTOs;
using Restore.Common.Utilities;
using Restore.Domain.Entities;

namespace Restore.Application.Baskets.Mappers;

public static class BasketMapper
{
    public static MethodResult<BasketDto> ToDto(Basket basket, IReadOnlyDictionary<Guid, Product> productLookup)
    {
        List<BasketItemDto> items = [];
        foreach (var item in basket.Items)
        {
            if (!productLookup.TryGetValue(item.ProductExtId, out var product))
            {
                return Result.NotFound<BasketDto>($"Product with id {item.ProductExtId} not found.");
            }
            items.Add(new BasketItemDto(
                ItemId: item.ExtId,
                ProductId: product.ExtId,
                Name: product.Name,
                Price: product.Price,
                PictureUrl: product.PictureUrl,
                Brand: product.Brand,
                Type: product.Type,
                Quantity: item.Quantity
            ));
        }
        return Result.Ok(new BasketDto(basket.ExtId, items));
    }
}

/* List<BasketItemDto> items = basket.Items.Select(item =>
        {
            if (!productLookup.TryGetValue(item.ProductXid, out var product))            
                throw new ProductNotFoundException(item.ProductXid);
            return new BasketItemDto(
                ItemId: item.ExtId,
                ProductId: product.ExtId,
                Name: product.Name,
                Price: product.Price,
                PictureUrl: product.PictureUrl,
                Brand: product.Brand,
                Type: product.Type,
                Quantity: item.Quantity
            );
        }).ToList();
        return new BasketDto(basket.ExtId, items); */


