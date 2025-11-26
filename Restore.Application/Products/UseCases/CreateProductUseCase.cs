using Restore.Application.Products.DTOs;
using Restore.Application.Products.Mappers;
using Restore.Application.Products.Interfaces;
using Restore.Common.Interfaces;


namespace Restore.Application.Products.UseCases;

public interface ICreateProductUseCase
{
    Task<ProductDto> ExecuteAsync(ProductDto dto);
}

public class CreateProductUseCase(
    IProductRepository productRepository,
    IUnitOfWork uow
    ) : ICreateProductUseCase
{
    // Inject repository interfaces
    public async Task<ProductDto> ExecuteAsync(ProductDto dto)
    {
        var product = await productRepository.AddAsync(dto.ToEntity());
        await uow.SaveChangesAsync();
        return product.ToDto();
    }
}


