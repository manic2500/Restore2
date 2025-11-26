using Restore.Application.Products.DTOs;
using Restore.Application.Products.Mappers;
using Restore.Application.Products.Interfaces;

namespace Restore.Application.Products.UseCases;

public interface IGetAllProductsUseCase
{
    Task<ProductDto[]> ExecuteAsync();
}
public class GetAllProductsUseCase(IProductRepository repository) : IGetAllProductsUseCase
{
    public async Task<ProductDto[]> ExecuteAsync()
    {
        var products = await repository.GetAllAsync();
        return products.ToDtoList();
    }
}
