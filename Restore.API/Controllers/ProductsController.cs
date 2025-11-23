using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;
using Restore.API.Responses;
using Restore.Application.DTO;
using Restore.Application.UseCases.Products;

namespace Restore.API.Controllers
{
    public class ProductsController(
        IGetAllProductsUseCase getAllProducts,
        IGetProductUseCase getProduct
        ) : BaseApiController
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await getAllProducts.ExecuteAsync();

            return Ok(new SuccessResponse<ProductDto[]>(products));
        }
        [HttpGet("{guid:guid}")]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid guid)
        {
            var product = await getProduct.ExecuteAsync(guid);
            return Ok(product);
        }
    }
}
