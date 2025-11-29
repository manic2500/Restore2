using Microsoft.AspNetCore.Mvc;
using Restore.API.Extensions;
using Restore.Application.Products.DTOs;
using Restore.Application.Products.UseCases;

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
