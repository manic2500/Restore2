using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restore.Application.DTO;
using Restore.Application.UseCases.Products;

namespace Restore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(
        IGetAllProductsUseCase getAllProducts,
        IGetProductUseCase getProduct
        ) : ControllerBase
    {

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            var products = await getAllProducts.ExecuteAsync();

            return Ok(products);
        }
        [HttpGet("{guid:guid}")]
        public async Task<ActionResult<ProductDto>> GetProductById(Guid guid)
        {
            var product = await getProduct.ExecuteAsync(guid);
            return Ok(product);
        }
    }
}
