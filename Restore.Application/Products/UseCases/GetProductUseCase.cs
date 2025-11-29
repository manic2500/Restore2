using Restore.Application.Products.DTOs;
using Restore.Application.Products.Mappers;
using Restore.Application.Products.Interfaces;
using Restore.Common.DTOs;
using Restore.Common.Utilities;
using Restore.Common.Extensions;

namespace Restore.Application.Products.UseCases;

public interface IGetProductUseCase
{
    Task<MethodResult<ProductDto>> ExecuteAsync(Guid guid);
}

public class GetProductUseCase(IProductRepository repository) : IGetProductUseCase
{
    public async Task<MethodResult<ProductDto>> ExecuteAsync(Guid guid)
    {
        var result = await repository.GetRequiredResultAsync(guid);
        if (!result.Success)
            return Result.Error<ProductDto>(result.Status, result.Error!);

        return Result.Ok(result.Data.ToDto());
    }
}
