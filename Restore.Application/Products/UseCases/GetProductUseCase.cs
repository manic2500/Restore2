using Restore.Application.Products.DTOs;
using Restore.Application.Products.Mappers;
using Restore.Application.Products.Interfaces;
using Restore.Domain.Exceptions;

namespace Restore.Application.Products.UseCases;

public interface IGetProductUseCase
{
    Task<ProductDto> ExecuteAsync(Guid guid);
}

public class GetProductUseCase(IProductRepository repository) : IGetProductUseCase
{
    public async Task<ProductDto> ExecuteAsync(Guid guid)
    {
        var product = await repository.GetByExIdAsync(guid)
                            ?? throw new ProductNotFoundException(guid);

        return product.ToDto();
    }
}
