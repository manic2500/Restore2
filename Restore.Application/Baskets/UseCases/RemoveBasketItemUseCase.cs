using Restore.Application.Baskets.Interfaces;
using Restore.Common.DTOs;
using Restore.Common.Interfaces;
using Restore.Common.Utilities;
using Restore.Common.Extensions;


namespace Restore.Application.Baskets.UseCases;

public interface IRemoveBasketItemUseCase
{
    Task<MethodResult> ExecuteAsync(Guid basketXid, Guid productXid);
}

public class RemoveBasketItemUseCase(IBasketRepository basketRepo, IUnitOfWork UoW) : IRemoveBasketItemUseCase
{
    public async Task<MethodResult> ExecuteAsync(Guid basketId, Guid itemId)
    {
        var basketResult = await basketRepo.GetRequiredResultAsync(basketId);
        if (!basketResult.Success)
            return Result.Error(basketResult.Status, basketResult.Error!);

        basketResult.Data.RemoveItem(itemId);

        await UoW.SaveChangesAsync();

        return Result.Ok();
    }
}
