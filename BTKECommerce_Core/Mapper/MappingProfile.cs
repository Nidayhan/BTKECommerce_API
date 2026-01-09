using AutoMapper;
using BTKECommerce_Core.DTOs.Category;
using BTKECommerce_Core.DTOs.Product;
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
            CreateMap<CategoryDTO,Category>().ReverseMap();
            #endregion



        }
    }
}
