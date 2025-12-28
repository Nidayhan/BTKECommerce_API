
using AutoMapper;
using BTKECommerce_core.DTOs.Category;
using BTKECommerce_core.DTOs.Product;
using BTKECommerce_core.Models;
using BTKECommerce_domain.Entities;

namespace BTKECommerce_core.Maper
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
            CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
            #endregion

        }
    }
}
