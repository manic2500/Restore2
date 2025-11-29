using Restore.Application.Baskets.Interfaces;
using Restore.Application.Products.Interfaces;
using Restore.Common.DTOs;
using Restore.Common.Extensions;
using Restore.Common.Interfaces;
using Restore.Common.Utilities;
using Restore.Domain.Entities;


namespace Restore.Application.Baskets.UseCases;

public interface IAddBasketItemUseCase
{
    Task<MethodResult> ExecuteAsync(Guid basketXid, Guid productXid, int quantity);
}

public class AddBasketItemUseCase(IBasketRepository basketRepo, IProductRepository productRepo, IUnitOfWork UoW) : IAddBasketItemUseCase
{

    public async Task<MethodResult> ExecuteAsync(Guid basketId, Guid productId, int quantity)
    {
        var basketResult = await basketRepo.GetRequiredResultAsync(basketId);
        if (!basketResult.Success)
            return Result.Error(basketResult.Status, basketResult.Error!);

        var product = await productRepo.GetByExIdAsync(productId);
        if (product is null)
            return Result.NotFound($"Product with id {productId} not found.");

        if (quantity <= 0)
            return Result.ValidationError("The quantity must be greater than 0.");

        if (product.QuantityInStock < quantity)
            return Result.ValidationError("Not enough stock available");

        var basket = basketResult.Data;
        basket.AddItem(new BasketItem
        {
            BasketId = basket.Id,
            ProductExtId = product.ExtId,
            UnitPrice = product.Price,
            Quantity = quantity
        });

        await UoW.SaveChangesAsync();
        return Result.Ok();
    }
}

/* if (basket.IsEmpty)
        {
            await basketRepo.DeleteAsync(basketXid); // hard delete
            return;
        } */
