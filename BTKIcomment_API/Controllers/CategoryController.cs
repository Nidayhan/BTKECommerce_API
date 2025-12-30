using BTKECommerce_core.DTOs.Category;
using BTKECommerce_core.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> CreateCategory(CategoryDTO model)
        {
            var isSaveChanges =await  _categoryService.CreateCategory(model);
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