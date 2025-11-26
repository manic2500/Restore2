using Restore.Application.Products.DTOs;
using Restore.Application.Products.Mappers;
using Restore.Application.Products.Interfaces;
using Restore.Common.Interfaces;


namespace Restore.Application.Products.UseCases;

public interface IUpdateProductUseCase
{
    Task<ProductDto> ExecuteAsync(ProductDto dto);
}

public class UpdateProductUseCase(IProductRepository repository, IUnitOfWork uow) : IUpdateProductUseCase
{
    public async Task<ProductDto> ExecuteAsync(ProductDto dto)
    {
        var product = await repository.UpdateByPublicIdAsync(dto.ToEntity());
        await uow.SaveChangesAsync();
        return product.ToDto();
    }
}

