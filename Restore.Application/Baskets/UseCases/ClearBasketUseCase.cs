using Restore.Application.Baskets.Interfaces;
using Restore.Common.DTOs;
using Restore.Common.Interfaces;
using Restore.Common.Utilities;
using Restore.Common.Extensions;


namespace Restore.Application.Baskets.UseCases;

public interface IClearBasketUseCase
{
    Task<MethodResult> ExecuteAsync(Guid basketId);
}

public class ClearBasketUseCase(IBasketRepository basketRepo, IUnitOfWork uow) : IClearBasketUseCase
{
    public async Task<MethodResult> ExecuteAsync(Guid basketId)
    {
        var basketResult = await basketRepo.GetRequiredResultAsync(basketId);
        if (!basketResult.Success)
            return Result.Error(basketResult.Status, basketResult.Error!);

        basketResult.Data.Clear();
        await uow.SaveChangesAsync();
        return Result.Ok();
    }
}

// Physically delete basket and all its items using ExecuteDeleteAsync()