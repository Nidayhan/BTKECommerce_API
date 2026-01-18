using BTKECommerce_Core.DTOs.Product;
using BTKECommerce_Core.DTOs.ProductImage;
using BTKECommerce_Domain.Migrations;
using BTKECommerce_Infrastructure.Models;
using static BTKECommerce_Core.DTOs.ProductImage.ProductImageDTO;

namespace BTKECommerce_Core.Services.Abstract
{
    public interface IProductService
    {
        Task<BaseResponseModel<bool>> CreateProduct(ProductDTO model);
        Task<BaseResponseModel<IEnumerable<ProductDTO>>> GetProducts(Guid categoryId);
        Task<BaseResponseModel<ProductImageDTO>> AddProductImage(Guid Id, AddProductImageDTO productImageDTO);
    }
}
