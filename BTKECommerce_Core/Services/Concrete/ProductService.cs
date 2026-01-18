using AutoMapper;
using BTKECommerce_Core.Constants;
using BTKECommerce_Core.DTOs.Product;
using BTKECommerce_Core.DTOs.ProductImage;
using BTKECommerce_Core.Services.Abstract;
using BTKECommerce_Domain.Entities;
using BTKECommerce_Domain.Interfaces;
using BTKECommerce_Infrastructure.Models;
using BTKECommerce_Infrastructure.UoW;
using Microsoft.EntityFrameworkCore;
using static BTKECommerce_Core.DTOs.ProductImage.ProductImageDTO;

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

        public async Task<BaseResponseModel<ProductImageDTO>> AddProductImage(Guid Id, AddProductImageDTO productImageDTO)
        {
            var product = await _unitOfWork.Products.GetAllAsyncExpression(
                predicate: x => x.Id == Id,
                includeExpressions: p => p.Include(pi => pi.ProductImages)
                );

            if (product is null)
            {
                return new BaseResponseModel<ProductImageDTO>()
                {
                    Success = false,
                    Message = Messages.NoDataFound,
                    Data = null
                };
            }
            if (productImageDTO.Image == null || productImageDTO.Image.Length == 0)
            {
                return new BaseResponseModel<ProductImageDTO>()
                {
                    Success = false,
                    Message = Messages.InvalidImage,
                    Data = null
                };
            }

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(productImageDTO.Image.FileName).ToLowerInvariant();
            //resim1.png
            //resim2.xlsx
            if (!allowedExtensions.Contains(extension))
            {
                return new BaseResponseModel<ProductImageDTO>
                {
                    Success = false,
                    Message = Messages.UnsupportedMediaType,
                };
            }
            var fileName = $"{Guid.NewGuid()}{extension}";
            var imagePath = Path.Combine("wwwroot", "images", fileName);
            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }
            var filePath = Path.Combine(imagePath, fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await productImageDTO.Image.CopyToAsync(stream);
            }

            var productImage = new ProductImage
            {
                ImageUrl = $"/images/{fileName}/{fileName}",
                ProductId = Id,
            };

            _unitOfWork.ProductImages.Add(productImage);

            await _unitOfWork.SaveChangesAsync();

            var productImageDto = _mapper.Map<ProductImageDTO>(productImage);

            return new BaseResponseModel<ProductImageDTO>
            {
                Data = productImageDto,
                Success = true,
                Message = Messages.SuccessCreateProductImage
            };
        }

        public async Task<BaseResponseModel<bool>> CreateProduct(ProductDTO model)
        {

            BaseResponseModel<bool> response = new();
            try
            {
                var obj = _mapper.Map<Product>(model);
                _unitOfWork.Products.Add(obj);
                if (await _unitOfWork.SaveChangesAsync() > 0)
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

        public async Task<BaseResponseModel<IEnumerable<ProductDTO>>> GetProducts(Guid categoryId)
        {

            if (categoryId.ToString().StartsWith("000"))
            {
                BaseResponseModel<IEnumerable<ProductDTO>> responseModel = new();
                var products = await _unitOfWork.Products.GetAllAsyncExpression(
                    null, includeExpressions: p => p.Include(pi => pi.ProductImages)
                    );
                var productDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);
                responseModel.Success = true;
                responseModel.Message = "Products Retrieved Succesfully";
                responseModel.Data = productDTO;
                return responseModel;
            }
            else
            {
                BaseResponseModel<IEnumerable<ProductDTO>> responseModel = new();
                var products = await _unitOfWork.Products.GetAllAsyncExpression(
                    predicate: p => p.CategoryId == categoryId,
                    includeExpressions: p => p.Include(pi => pi.ProductImages)
                    );
                var productDTO = _mapper.Map<IEnumerable<ProductDTO>>(products);
                responseModel.Success = true;
                responseModel.Message = "Products Retrieved Succesfully";
                responseModel.Data = productDTO;
                return responseModel;
            }


        }
    }
}