using Restore.Application.Baskets.Interfaces;
using Restore.Application.Products.Interfaces;
using Restore.Common.Exceptions;
using Restore.Common.Interfaces;
using Restore.Domain.Entities;
using Restore.Domain.Exceptions;

namespace Restore.Application.Baskets.UseCases;

public interface IAddBasketItemUseCase
{
    Task ExecuteAsync(Guid basketXid, Guid productXid, int quantity);
}

public class AddBasketItemUseCase(IBasketRepository basketRepo, IProductRepository productRepo, IUnitOfWork UoW) : IAddBasketItemUseCase
{

    public async Task ExecuteAsync(Guid basketId, Guid productId, int quantity)
    {
        var basket = await basketRepo.GetByExIdAsync(basketId) ?? throw new BasketNotFoundException(basketId);

        var product = await productRepo.GetByExIdAsync(productId) ?? throw new ProductNotFoundException(productId);

        if (quantity <= 0) throw new BusinessException("The quantity must be greater than 0.");

        if (product.QuantityInStock < quantity) throw new BusinessException("Not enough stock available");

        basket.AddItem(new BasketItem
        {
            BasketId = basket.Id,
            ProductXid = product.ExtId,
            UnitPrice = product.Price,
            Quantity = quantity
        });

        await UoW.SaveChangesAsync();
    }
}

/* if (basket.IsEmpty)
        {
            await basketRepo.DeleteAsync(basketXid); // hard delete
            return;
        } */
