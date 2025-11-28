using System;
using Restore.Application.Baskets.DTOs;
using Restore.Domain.Entities;
using Restore.Domain.Exceptions;

namespace Restore.Application.Baskets.Mappers;

public static class BasketMapper
{
    /// <summary>
    /// Maps Basket → BasketDto using live product data.
    /// </summary>
    public static BasketDto ToDto(Basket basket, IReadOnlyDictionary<Guid, Product> productLookup)
    {
        var items = basket.Items.Select(item =>
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

        return new BasketDto(basket.ExtId, items);
        /* {
            Discount = basket.Discount,
            Shipping = basket.Shipping,
            Tax = basket.Tax
        }; */
    }
}

