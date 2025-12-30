using BTKECommerce_core.DTOs.Category;
using BTKECommerce_domain.Entities;
using BTKECommerce_Infrastructure.Models;

namespace BTKECommerce_core.Services.Abstract
{
    public interface ICategoryService
    {
        Task<BaseResponseModel<bool>> CreateCategory(CategoryDTO model);
        Task<BaseResponseModel<bool>> DeleteCategory(Guid Id);
        Task<BaseResponseModel<List<Category>>> GetCategories();
        Task<BaseResponseModel<Category>> GetCategoryById(Guid Id);
        Task<BaseResponseModel<Category>> UpdateCategory(Guid Id, CategoryDTO model);
        
        Task<BaseResponseModel<List<Category>>> GetProductsByCategory();
    }
}
