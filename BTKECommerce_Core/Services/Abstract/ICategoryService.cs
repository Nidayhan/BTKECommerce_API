using BTKECommerce_Core.DTOs.Category;
using BTKECommerce_Domain.Entities;
using BTKECommerce_Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_Core.Services.Abstract
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
