using Restore.Application.Baskets.Interfaces;
using Restore.Common.Interfaces;
using Restore.Domain.Exceptions;

namespace Restore.Application.Baskets.UseCases;

public interface IClearBasketUseCase
{
    Task ExecuteAsync(Guid basketId);
}

public class ClearBasketUseCase(IBasketRepository repository, IUnitOfWork uow) : IClearBasketUseCase
{
    public async Task ExecuteAsync(Guid basketId)
    {
        var basket = await repository.GetByExIdAsync(basketId) ?? throw new BasketNotFoundException(basketId);
        basket.Clear();
        await uow.SaveChangesAsync();
    }
}

// Physically delete basket and all its items using ExecuteDeleteAsync()