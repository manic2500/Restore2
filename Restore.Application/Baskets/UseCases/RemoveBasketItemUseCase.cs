using Restore.Application.Baskets.Interfaces;
using Restore.Common.Exceptions;
using Restore.Common.Interfaces;
using Restore.Domain.Exceptions;

namespace Restore.Application.Baskets.UseCases;

public interface IRemoveBasketItemUseCase
{
    Task ExecuteAsync(Guid basketXid, Guid productXid);
}

public class RemoveBasketItemUseCase(IBasketRepository basketRepo, IUnitOfWork UoW) : IRemoveBasketItemUseCase
{
    public async Task ExecuteAsync(Guid basketId, Guid itemId)
    {
        var basket = await basketRepo.GetByExIdAsync(basketId) ?? throw new BasketNotFoundException(basketId);
        basket.RemoveItem(itemId);

        /* if (basket.IsEmpty)
        {
            await basketRepo.DeleteAsync(basketId);
        } */

        await UoW.SaveChangesAsync();
    }
}
