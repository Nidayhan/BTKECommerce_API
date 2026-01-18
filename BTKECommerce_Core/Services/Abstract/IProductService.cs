using BTKECommerce_Core.DTOs.Product;
using BTKECommerce_Infrastructure.Models;

namespace BTKECommerce_Core.Services.Abstract
{
    public interface IProductService
    {
        Task<BaseResponseModel<bool>> CreateProduct(ProductDTO model);
        Task<BaseResponseModel<IEnumerable<ProductDTO>>> GetProducts(Guid categoryId);

    }
}
