using AutoMapper;
using BTKECommerce_core.DTOs.Category;
using BTKECommerce_core.Services.Abstract;
using BTKECommerce_Core.Constants;
using BTKECommerce_domain.Entities;
using BTKECommerce_Infrastructure.Models;
using BTKECommerce_Infrastructure.UoW;

namespace BTKECommerce_Core.Services.Concrete
{
    public class CategoryService : ICategoryService
    {

        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public BaseResponseModel<bool> CreateCategory(CategoryDTO model)
        {
            BaseResponseModel<bool> response = new BaseResponseModel<bool>();
            var objDTO = _mapper.Map<Category>(model);
            categoryRepository.Add(objDTO);
            response.Data = true;
            response.Message = Messages.SuccessCreateCategory;
            response.Success = true;
            return response;


        }

        public async Task<BaseResponseModel<bool>> DeleteCategory(Guid Id)
        {
            try
            {
                //var obj = _context.Categories.FirstOrDefault(x => x.Id == Id);
                var obj = await categoryRepository.GetById(Id);
                categoryRepository.Delete(obj);
                return new BaseResponseModel<bool>
                {
                    Data = true,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                // Log the exception (ex) as neede
                Console.WriteLine(ex.Message);
                return new BaseResponseModel<bool>
                {
                    Data = false,
                    Success = false
                };
            }
        }

        public async Task<BaseResponseModel<List<Category>>> GetCategories()
        {
            var list = categoryRepository.GetAll().Result.ToList();
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
                Data = await categoryRepository.GetById(Id)
            };

        }

        public async Task<BaseResponseModel<Category>> UpdateCategory(Guid Id, CategoryDTO model)
        {
            //Önce parametreden gelen id'yi için Categories tablosundaki eşleşen kaydı bulacağız.
            Category category = await categoryRepository.GetById(Id);
            //mevcut verileri parametreden gelen güncel veriler ile güncelleyeceğiz.
            _mapper.Map(model, category);
            //context'e güncel nesneyi kaydedeceğiz.
            categoryRepository.Update(category);
            //veritabanına değişiklikleri kaydedeceğiz.
            return new BaseResponseModel<Category>
            {
                Data = category,
                Message = Messages.SaveChangesSuccess,
                Success = true
            };
            //return new BaseResponseModel<Category>
            //{
            //    Data = null,
            //    Success = false,
            //    Message = Messages.SaveChangesFail
            //};

        }



    }
}