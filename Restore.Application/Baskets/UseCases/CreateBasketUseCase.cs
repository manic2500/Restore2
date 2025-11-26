using Restore.Application.Baskets.Interfaces;
using Restore.Common.Interfaces;
using Restore.Domain.Entities;

namespace Restore.Application.Baskets.UseCases;

public interface ICreateBasketUseCase
{
    Task<Guid> ExecuteAsync();
}

public class CreateBasketUseCase(IBasketRepository basketRepo, IUnitOfWork UoW) : ICreateBasketUseCase
{
    public async Task<Guid> ExecuteAsync()
    {
        var basket = new Basket();
        await basketRepo.AddAsync(basket);
        await UoW.SaveChangesAsync();
        return basket.Xid;
    }
}
