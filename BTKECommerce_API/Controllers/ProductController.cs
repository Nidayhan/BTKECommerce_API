using BTKECommerce_Core.DTOs.Product;
using BTKECommerce_Core.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static BTKECommerce_Core.DTOs.ProductImage.ProductImageDTO;

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
        public async Task<IActionResult> CreateProduct(ProductRequestDTO productDTO)
        {
            var result = await _productService.CreateProduct(productDTO);
            return Ok(result);
        }
        [HttpGet("GetProductsByCategoryId")]
        public async Task<IActionResult> GetProductsByCategoryId(Guid categoryId)
        {
            var result = await _productService.GetProducts(categoryId);
            return Ok(result);

        }

        [HttpPost("{id}/images")]
        public async Task<IActionResult> AddProductImage(Guid id, [FromForm] AddProductImageDTO imageDto)
        {
            var result = await _productService.AddProductImage(id, imageDto);
            if (!result.Success) return BadRequest(result);
            return Ok(result);


        }
    }
}