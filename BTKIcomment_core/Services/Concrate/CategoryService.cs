using AutoMapper;
using BTKECommerce_core.Constants;
using BTKECommerce_core.DTOs.Category;
using BTKECommerce_core.Services.Abstract;
using BTKECommerce_domain.Data;
using BTKECommerce_domain.Entities;
using BTKECommerce_Infrastructure.Models;

namespace BTKECommerce_Core.Services.Concrete
{
    public class CategoryService : ICategoryService
    {

        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, ApplicationDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public BaseResponseModel<bool> CreateCategory(CategoryDTO model)
        {
            BaseResponseModel<bool> response = new BaseResponseModel<bool>();
            var objDTO = _mapper.Map<Category>(model);
            _context.Categories.Add(objDTO);
            if (_context.SaveChanges() > 0)
            {
                response.Data = true;
                response.Message = Messages.SuccessCreateCategory;
                response.Success = true;
                return response;
            }
            return new BaseResponseModel<bool>
            {
                Data = false,
                Message = Messages.FailCreateCategory,
                Success = false
            };
        }

        public BaseResponseModel<bool> DeleteCategory(Guid Id)
        {

            try
            {
                var obj = _context.Categories.FirstOrDefault(x => x.Id == Id);
                _context.Categories.Remove(obj);
                _context.SaveChanges();
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

        public BaseResponseModel<List<Category>> GetCategories()
        {
            return new BaseResponseModel<List<Category>>
            {
                Data = _context.Categories.ToList(),
            };
        }

        public BaseResponseModel<Category> GetCategoryById(Guid Id)
        {

            //Önce parametreden gelen id'yi için Categories tablosundaki eşleşen kaydı bulacağız.

            //Bulduğumuz kaydı döneceğiz.
            return new BaseResponseModel<Category>
            {
                Data = _context.Categories.Find(Id)
            };

        }

        public BaseResponseModel<Category> UpdateCategory(Guid Id, CategoryDTO model)
        {
            //Önce parametreden gelen id'yi için Categories tablosundaki eşleşen kaydı bulacağız.
            Category category = _context.Categories.Find(Id);
            //mevcut verileri parametreden gelen güncel veriler ile güncelleyeceğiz.
            _mapper.Map(model, category);
            //context'e güncel nesneyi kaydedeceğiz.
            _context.Categories.Update(category);
            //veritabanına değişiklikleri kaydedeceğiz.
            if (_context.SaveChanges() > 0)
            {
                //güncellenen kategoriyi döneceğiz.
                return new BaseResponseModel<Category>
                {
                    Data = category,
                    Message = Messages.SaveChangesSuccess,
                    Success = true
                };
            }
            return new BaseResponseModel<Category>
            {
                Data = null,
                Success = false,
                Message = Messages.SaveChangesFail
            };

        }



    }
}