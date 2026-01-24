using BTKECommerce_Core.DTOs.Basket;
using BTKECommerce_Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_Core.Services.Abstract
{
    public interface IBasketService
    {
        Task<bool> AddToBasket(BasketDTO dto);
        Task<BaseResponseModel<BasketResponseDTO>> GetBasketItemByUserId(string userId);
    }
}
