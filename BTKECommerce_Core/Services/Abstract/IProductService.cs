using BTKECommerce_Core.DTOs.Category;
using BTKECommerce_Core.DTOs.Product;
using BTKECommerce_Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_Core.Services.Abstract
{
    public interface IProductService
    {
        Task<BaseResponseModel<bool>> CreateProduct(ProductDTO model);

    }
}
