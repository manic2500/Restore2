using Restore.Application.Baskets.Interfaces;
using Restore.Common.DTOs;
using Restore.Common.Interfaces;
using Restore.Common.Utilities;
using Restore.Common.Extensions;


namespace Restore.Application.Baskets.UseCases;

public interface IIncreaseBasketItemUseCase
{
    Task<MethodResult> ExecuteAsync(Guid basketXid, Guid productXid);
}

public class IncreaseBasketItemUseCase(IBasketRepository basketRepo, IUnitOfWork uow) : IIncreaseBasketItemUseCase
{
    public async Task<MethodResult> ExecuteAsync(Guid basketId, Guid itemId)
    {
        var basketResult = await basketRepo.GetRequiredResultAsync(basketId);
        if (!basketResult.Success)
            return Result.Error(basketResult.Status, basketResult.Error!);

        basketResult.Data.IncreaseItem(itemId);

        await uow.SaveChangesAsync();

        return Result.Ok();
    }
}

/* var basket = await basketRepo.GetByExIdAsync(basketId);
        if (basket is null)
            return Result.NotFound($"Basket with id '{basketId}' not found.");  */
