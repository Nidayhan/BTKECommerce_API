using BTKECommerce_core.DTOs.Category;
using BTKECommerce_domain.Entities;
using BTKECommerce_Infrastructure.Models;

namespace BTKECommerce_core.Services.Abstract
{
    public interface ICategoryService
    {
        BaseResponseModel<bool> CreateCategory(CategoryDTO model);
       
        BaseResponseModel<bool> GetCategories();
        BaseResponseModel<List<Category>>  GetCategoryById(Guid Id);
        BaseResponseModel<bool> UpdateCategory(int Id, CategoryDTO model);
        BaseResponseModel<bool> DeleteCategory(Guid Id);
    }
}
