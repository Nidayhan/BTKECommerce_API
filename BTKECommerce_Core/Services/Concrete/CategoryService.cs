using AutoMapper;
using Azure;
using BTKECommerce_Core.Constants;
using BTKECommerce_Core.DTOs.Category;
using BTKECommerce_Core.Services.Abstract;
using BTKECommerce_Domain.Data;
using BTKECommerce_Domain.Entities;
using BTKECommerce_Domain.Interfaces;
using BTKECommerce_Infrastructure.Models;
using BTKECommerce_Infrastructure.UoW;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BTKECommerce_Core.Services.Concrete
{
    public class CategoryService : ICategoryService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<CategoryDTO> _validator;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CategoryService(IMapper mapper,IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor,IValidator<CategoryDTO> _validator)
        {
            _httpContextAccessor = httpContextAccessor;
            this._validator = _validator;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponseModel<bool>> CreateCategory(CategoryDTO model)
        {
            BaseResponseModel<bool> response = new BaseResponseModel<bool>();
            List<string> errorMessages = new();
            var result = _validator.Validate(model);
            if (!result.IsValid)
            {
                //var errors = result.Errors.ForEach(x => x.ErrorMessage);
                response.Data = false;
                response.Message = Messages.FailCreateCategory;
                response.Success = false;
                errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                response.ErrorMessages = errorMessages;
                return response;
            }
            string userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            model.CreatedBy = userId;

            var objDTO = _mapper.Map<Category>(model);
            _unitOfWork.Categories.Add(objDTO);
            if(await _unitOfWork.SaveChangesAsync() > 0)
            {
                response.Data = true;
                response.Message = Messages.SuccessCreateCategory;
                response.Success = true;
                return response;
            }


            response.Data = false;
            response.Message = Messages.FailCreateCategory;
            response.Success = false;
            return response;
        }

        public async Task<BaseResponseModel<bool>> DeleteCategory(Guid Id)
        {
            try
            {
                //var obj = _context.Categories.FirstOrDefault(x => x.Id == Id);
                var obj = await _unitOfWork.Categories.GetById(Id);
                _unitOfWork.Categories.Delete(obj);
                if (await _unitOfWork.SaveChangesAsync() > 0)
                {
                    return new BaseResponseModel<bool>
                    {
                        Data = true,
                        Success = true
                    };
                }
                return new BaseResponseModel<bool>
                {
                    Data = false,
                    Success = false
                };
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as neede
                Console.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<BaseResponseModel<List<Category>>> GetCategories()
        {
            var list = _unitOfWork.Categories.GetAll().Result.ToList();
            return new BaseResponseModel<List<Category>>
            {
                Data = list
            };
        }

        public async Task<BaseResponseModel<Category>> GetCategoryById(Guid Id)
        {

            //Önce parametreden gelen id'yi için Categories tablosundaki eşleşen kaydı bulacağız.
         
            //Bulduğumuz kaydı döneceğiz.
            return new BaseResponseModel<Category>
            {
                Data = await _unitOfWork.Categories.GetById(Id)
            };

        }

        public async Task<BaseResponseModel<List<Category>>> GetProductsByCategory()
        {
            BaseResponseModel<List<Category>> response = new BaseResponseModel<List<Category>>();
            var result = await _unitOfWork.Categories.GetAllAsyncWithInclude(x => x.Include(x => x.Products));
            //var categories = result.Where(x => x.Id == Id).ToList();
            if (result.Count() > 0)
            {
                response.Data = result.ToList();
                response.Success = true;
                return response;
            }
            response.Data = new List<Category>();
            response.Success = true;
            response.Message = Messages.NoDataFound;
            return response;
        }

        public async Task<BaseResponseModel<Category>> UpdateCategory(Guid Id, CategoryDTO model)
        {
            //Önce parametreden gelen id'yi için Categories tablosundaki eşleşen kaydı bulacağız.
            Category category = await _unitOfWork.Categories.GetById(Id);
            //mevcut verileri parametreden gelen güncel veriler ile güncelleyeceğiz.
            _mapper.Map(model, category);
            //context'e güncel nesneyi kaydedeceğiz.
            _unitOfWork.Categories.Update(category);
            //veritabanına değişiklikleri kaydedeceğiz.
            if (await _unitOfWork.SaveChangesAsync() > 0)
            {
                return new BaseResponseModel<Category>
                {
                    Data = category,
                    Message = Messages.SaveChangesSuccess,
                    Success = true
                };


            }
            return new BaseResponseModel<Category>
            {
                Data = new Category(),
                Message = Messages.SaveChangesFail,
                Success = false
            };
        }


    }
}
