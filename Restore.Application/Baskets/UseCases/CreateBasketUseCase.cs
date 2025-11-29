using Restore.Application.Baskets.Interfaces;
using Restore.Common.DTOs;
using Restore.Common.Interfaces;
using Restore.Common.Utilities;
using Restore.Domain.Entities;

namespace Restore.Application.Baskets.UseCases;

public interface ICreateBasketUseCase
{
    Task<MethodResult<Guid>> ExecuteAsync();
}

public class CreateBasketUseCase(IBasketRepository basketRepo, IUnitOfWork UoW) : ICreateBasketUseCase
{
    public async Task<MethodResult<Guid>> ExecuteAsync()
    {
        var basket = new Basket();
        await basketRepo.AddAsync(basket);
        await UoW.SaveChangesAsync();
        return Result.Ok(basket.ExtId);
    }
}
