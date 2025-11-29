using Restore.Application.Baskets.Interfaces;
using Restore.Common.DTOs;
using Restore.Common.Utilities;
using Restore.Domain.Entities;

namespace Restore.Application.Baskets.Extensions;

/* public static class BasketRepositoryExtension
{
    public static async Task<MethodResult<Basket>> GetRequiredResultAsync(
            this IBasketRepository repo,
            Guid basketId)
    {
        var basket = await repo.GetByExIdAsync(basketId);

        return basket is null
            ? Result.NotFound<Basket>($"Basket with id '{basketId}' not found.")
            : Result.Ok(basket);
    }
} */
