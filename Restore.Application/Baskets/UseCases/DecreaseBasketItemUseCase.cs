using Restore.Application.Baskets.Interfaces;
using Restore.Common.Exceptions;
using Restore.Common.Interfaces;
using Restore.Domain.Exceptions;

namespace Restore.Application.Baskets.UseCases;

public interface IDecreaseBasketItemUseCase
{
    Task ExecuteAsync(Guid basketXid, Guid productXid);
}

public class DecreaseBasketItemUseCase(IBasketRepository basketRepo, IUnitOfWork uow) : IDecreaseBasketItemUseCase
{
    public async Task ExecuteAsync(Guid basketId, Guid itemId)
    {
        var basket = await basketRepo.GetByExIdAsync(basketId) ?? throw new BasketNotFoundException(basketId);

        basket.DecreaseItem(itemId);

        await uow.SaveChangesAsync();
    }
}
