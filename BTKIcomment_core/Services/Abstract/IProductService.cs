
using BTKECommerce_Core.DTOs.Product;
using BTKECommerce_Infrastructure.Models;

namespace BTKECommerce_core.Services.Abstract
{
    public interface IProductService
    {
        Task<BaseResponseModel<bool>> CreateProduct(ProductDTO model);
    }
}
