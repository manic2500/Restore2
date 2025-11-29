using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Restore.API.Extensions;
using Restore.API.Responses;
using Restore.Application.Products.DTOs;
using Restore.Application.Products.UseCases;
using Restore.Common.DTOs;

namespace Restore.API.Controllers
{
    public class ProductsController(
        IGetAllProductsUseCase getAllProducts,
        IGetProductUseCase getProduct
        ) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<ProductDto[]>> GetProducts()
        {
            var result = await getAllProducts.ExecuteAsync();
            return this.ToActionResult(result);
            //return Ok(new SuccessResponse<ProductDto[]>(result.Data));
        }
        [HttpGet("{guid:guid}")]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid guid)
        {
            var result = await getProduct.ExecuteAsync(guid);
            return this.ToActionResult(result);
        }
    }
}
