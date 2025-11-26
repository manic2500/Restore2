using Restore.Application.Baskets.DTOs;
using Restore.Application.Baskets.Interfaces;
using Restore.Application.Products.Interfaces;
using Restore.Application.Products.Mappers;
using Restore.Common.Exceptions;
using Restore.Domain.Exceptions;

namespace Restore.Application.Baskets.UseCases;

public interface IGetBasketUseCase
{
    Task<BasketDto> ExecuteAsync(Guid basketXid);
}

public class GetBasketUseCase(IBasketRepository basketRepo, IProductRepository productRepo) : IGetBasketUseCase
{
    public async Task<BasketDto> ExecuteAsync(Guid basketId)
    {
        var basket = await basketRepo.GetByXidAsync(basketId) ?? throw new BasketNotFoundException(basketId);

        var productIds = basket.Items.Select(i => i.ProductXid).ToList(); // Get all product IDs from the basket items

        var products = await productRepo.GetByXidsAsync(productIds); // Fetch live product data - returns List<Product>

        var productDict = products.ToDictionary(p => p.Xid); // Build a lookup dictionary: Xid → Product

        var items = basket.Items.Select(item =>
        {
            if (!productDict.TryGetValue(item.ProductXid, out var product))
                throw new NotFoundException($"Product {item.ProductXid} not found");

            return new BasketItemDto(
                            product.Xid,
                            product.Name,
                            product.Price,
                            product.PictureUrl,
                            product.Brand,
                            product.Type,
                            item.Quantity
            );
        }).ToList();

        return new BasketDto(basket.Xid, items);
    }
}

/* 

var basketDto = new BasketDto
{
    Xid = basket.Xid,
    Items = [.. basket.Items.Select(i =>
    {
        var product = products.Single(p => p.Xid == i.ProductXid);
        return new BasketItemDto
        {
            Product = product.ToDto(),
            Quantity = i.Quantity
        };
    })]
}; 

 */

