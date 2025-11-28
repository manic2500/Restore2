using Restore.Application.Baskets.Interfaces;
using Restore.Common.Interfaces;
using Restore.Domain.Exceptions;

namespace Restore.Application.Baskets.UseCases;

public interface IRemoveVoucherUseCase
{
    Task ExecuteAsync(Guid basketId);
}

public class RemoveVoucherUseCase(IBasketRepository basketRepo, IUnitOfWork uow) : IRemoveVoucherUseCase
{
    public async Task ExecuteAsync(Guid basketId)
    {
        // 1. Load basket
        var basket = await basketRepo.GetByXidAsync(basketId) ?? throw new BasketNotFoundException(basketId);
        basket.VoucherCode = null;
        await uow.SaveChangesAsync();
    }
}
