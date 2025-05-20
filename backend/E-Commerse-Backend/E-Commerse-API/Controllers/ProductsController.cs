using E_Commerse_Core.Interfaces;
using E_Commerse_Data.DTO;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerse_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("GetAllProducts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllProducts()
        {
            IList<ProductDTO> products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
    }
}
