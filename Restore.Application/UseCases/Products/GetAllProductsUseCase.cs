using System;
using Restore.Application.DTO;
using Restore.Application.Interfaces;
using Restore.Application.Mappers;
using Restore.Domain.Exceptions;

namespace Restore.Application.UseCases.Products;

public interface IGetAllProductsUseCase
{
    Task<ProductDto[]> ExecuteAsync();
}
public class GetAllProductsUseCase(IProductRepository productRepository) : IGetAllProductsUseCase
{
    public async Task<ProductDto[]> ExecuteAsync()
    {
        var products = await productRepository.GetAllAsync();
        return products.ToDtoList();
    }
}

public interface IGetProductUseCase
{
    Task<ProductDto> ExecuteAsync(Guid guid);
}

public class GetProductUseCase(IProductRepository productRepository) : IGetProductUseCase
{
    public async Task<ProductDto> ExecuteAsync(Guid guid)
    {
        var product = await productRepository.GetByPublicIdAsync(guid)
                            ?? throw new ProductNotFoundException(guid);

        return product.ToDto();
    }
}
