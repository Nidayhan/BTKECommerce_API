using AutoMapper;
using BTKECommerce_Core.DTOs.Basket;
using BTKECommerce_Core.DTOs.Category;
using BTKECommerce_Core.DTOs.Product;
using BTKECommerce_Core.DTOs.ProductImage;
using BTKECommerce_Domain.Entities;

namespace BTKECommerce_Core.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Product Mappings
            CreateMap<ProductDTO, Product>().ReverseMap();
            #endregion

            #region Category Mappings
            CreateMap<CategoryDTO, Category>().ReverseMap();
            #endregion


            #region ProductImage
            CreateMap<ProductImage, ProductImageDTO>().ReverseMap();
            #endregion

            CreateMap<Basket, BasketDTO>().ReverseMap();
            CreateMap<BasketItem, BasketItemResponseDTO>().ReverseMap();
            CreateMap<Basket, BasketResponseDTO>().ReverseMap();
        }
    }
}