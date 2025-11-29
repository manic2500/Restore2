using Restore.Application.Products.DTOs;
using Restore.Application.Products.Mappers;
using Restore.Application.Products.Interfaces;
using Restore.Common.Interfaces;
using Restore.Common.DTOs;
using Restore.Common.Extensions;
using Restore.Common.Utilities;


namespace Restore.Application.Products.UseCases;

public interface IUpdateProductUseCase
{
    Task<MethodResult<ProductDto>> ExecuteAsync(Guid productId, ProductDto dto);
}

public class UpdateProductUseCase(IProductRepository repository, IUnitOfWork uow) : IUpdateProductUseCase
{
    public async Task<MethodResult<ProductDto>> ExecuteAsync(Guid productId, ProductDto dto)
    {
        //var product = await repository.UpdateByPublicIdAsync(dto.ToEntity());
        var result = await repository.GetRequiredResultAsync(productId);
        if (!result.Success)
            return Result.Error<ProductDto>(result.Status, result.Error!);

        var existing = result.Data;

        existing.Name = dto.Name;
        existing.QuantityInStock = dto.QuantityInStock;
        existing.Price = dto.Price;

        await uow.SaveChangesAsync();
        return Result.Ok(existing.ToDto());
    }
}

