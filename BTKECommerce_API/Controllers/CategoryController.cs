using BTKECommerce_Core.DTOs.Category;
using BTKECommerce_Core.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace BTKECommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
     
     
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

   

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _categoryService.GetProductsByCategory());
        }

        [HttpPost]
        [Authorize(Roles ="Admin,User")]
        public async Task<IActionResult> CreateCategory(CategoryDTO model)
        {
          
            var isSaveChanges = await _categoryService.CreateCategory(model);
            return Ok(isSaveChanges);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(Guid Id)
        {
            var category = _categoryService.DeleteCategory(Id);
            return Ok(category);
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> UpdateCategory([FromRoute]Guid Id, CategoryDTO dto)
        {
            var category = _categoryService.UpdateCategory(Id, dto);
            return Ok(category);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid Id)
        {
            var category = _categoryService.GetCategoryById(Id);
            return Ok(category);
        }

      

    }


  


}
