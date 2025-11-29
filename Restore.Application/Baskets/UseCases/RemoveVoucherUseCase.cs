using Restore.Application.Baskets.Interfaces;
using Restore.Common.DTOs;
using Restore.Common.Interfaces;
using Restore.Common.Utilities;
using Restore.Common.Extensions;


namespace Restore.Application.Baskets.UseCases;

public interface IRemoveVoucherUseCase
{
    Task<MethodResult> ExecuteAsync(Guid basketId);
}

public class RemoveVoucherUseCase(IBasketRepository basketRepo, IUnitOfWork uow) : IRemoveVoucherUseCase
{
    public async Task<MethodResult> ExecuteAsync(Guid basketId)
    {
        // 1. Load basket
        //var basket = await basketRepo.GetByExIdAsync(basketId) ?? throw new BasketNotFoundException(basketId);
        var basketResult = await basketRepo.GetRequiredResultAsync(basketId);
        if (!basketResult.Success)
            return Result.Error(basketResult.Status, basketResult.Error!);

        var basket = basketResult.Data;

        basket.VoucherCode = null;
        await uow.SaveChangesAsync();
        return Result.Ok();
    }
}
