using BTKECommerce_core.Services.Abstract;
using BTKECommerce_Core.DTOs.Product;
using Microsoft.AspNetCore.Mvc;

namespace BTKECommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO productDTO)
        {
            var result = _productService.CreateProduct(productDTO);
            return Ok(result);
        }


    }
}