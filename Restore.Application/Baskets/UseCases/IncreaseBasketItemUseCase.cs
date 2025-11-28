using Restore.Application.Baskets.Interfaces;
using Restore.Common.Exceptions;
using Restore.Common.Interfaces;
using Restore.Domain.Exceptions;

namespace Restore.Application.Baskets.UseCases;

public interface IIncreaseBasketItemUseCase
{
    Task ExecuteAsync(Guid basketXid, Guid productXid);
}

public class IncreaseBasketItemUseCase(IBasketRepository basketRepo, IUnitOfWork uow) : IIncreaseBasketItemUseCase
{
    public async Task ExecuteAsync(Guid basketId, Guid itemId)
    {
        var basket = await basketRepo.GetByExIdAsync(basketId) ?? throw new BasketNotFoundException(basketId);

        basket.IncreaseItem(itemId);

        await uow.SaveChangesAsync();
    }
}
