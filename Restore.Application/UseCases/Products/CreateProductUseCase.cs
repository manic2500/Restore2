using Restore.Application.DTO;
using Restore.Application.Interfaces;
using Restore.Application.Mappers;

namespace Restore.Application.UseCases.Products;

public interface ICreateProductUseCase
{
    Task<ProductDto> ExecuteAsync(ProductDto dto);
}

public class CreateProductUseCase(IProductRepository productRepository) : ICreateProductUseCase
{
    // Inject repository interfaces
    public async Task<ProductDto> ExecuteAsync(ProductDto dto)
    {
        var product = await productRepository.AddAsync(dto.ToEntity());
        return product.ToDto();
    }
}

