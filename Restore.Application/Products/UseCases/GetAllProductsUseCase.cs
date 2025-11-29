using Restore.Application.Products.DTOs;
using Restore.Application.Products.Mappers;
using Restore.Application.Products.Interfaces;
using Restore.Common.DTOs;
using Restore.Common.Utilities;

namespace Restore.Application.Products.UseCases;

public interface IGetAllProductsUseCase
{
    Task<MethodResult<ProductDto[]>> ExecuteAsync();
}
public class GetAllProductsUseCase(IProductRepository repository) : IGetAllProductsUseCase
{
    public async Task<MethodResult<ProductDto[]>> ExecuteAsync()
    {
        var products = await repository.GetAllAsync();
        return Result.Ok(products.ToDtoList());
    }
}
