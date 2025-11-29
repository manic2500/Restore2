using Restore.Common.DTOs;
using Restore.Common.Interfaces;
using Restore.Common.Utilities;


namespace Restore.Common.Extensions;

public static class GenericRepositoryExtension
{
    public static async Task<MethodResult<T>> GetRequiredResultAsync<T>(
            this IGenericRepository<T> repo,
            Guid guid) where T : class
    {
        var entity = await repo.GetByExIdAsync(guid);

        return entity is null
            ? Result.NotFound<T>($"{typeof(T).Name} with id '{guid}' not found.")
            : Result.Ok(entity);
    }
}
/* public static class ProductRepositoryExtension
{
    public static async Task<MethodResult<Product>> GetRequiredResultAsync(
            this IProductRepository repo,
            Guid productId)
    {
        var product = await repo.GetByExIdAsync(productId);

        return product is null
            ? Result.NotFound<Product>($"Basket with id '{productId}' not found.")
            : Result.Ok(product);
    }
}
 */