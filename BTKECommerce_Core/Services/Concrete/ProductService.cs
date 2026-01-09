using AutoMapper;
using BTKECommerce_Core.Constants;
using BTKECommerce_Core.DTOs.Product;
using BTKECommerce_Core.Services.Abstract;
using BTKECommerce_Domain.Entities;
using BTKECommerce_Domain.Interfaces;
using BTKECommerce_Infrastructure.Models;
using BTKECommerce_Infrastructure.UoW;

namespace BTKECommerce_Core.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ProductService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponseModel<bool>> CreateProduct(ProductDTO model)
        {

            BaseResponseModel<bool> response = new();
            try
            {
                var obj = _mapper.Map<Product>(model);
                _unitOfWork.Products.Add(obj);
                if(await _unitOfWork.SaveChangesAsync() > 0)
                {
                    response.Message = Messages.SuccessCreateProduct;
                    response.Data = true;
                    response.Success = true;
                    return response;
                }
                response.Message = Messages.FailCreateProduct;
                response.Data = false;
                response.Success = false;
                return response;
            }
            catch (Exception ex)
            {
                response.Message = Messages.FailCreateProduct;
                response.Data = false;
                response.Success = false;
                return response;
            }
        }
    }
}
