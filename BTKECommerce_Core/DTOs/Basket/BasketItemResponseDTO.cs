using BTKECommerce_Core.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_Core.DTOs.Basket
{
    public class BasketItemResponseDTO
    {
        public Guid ProductId { get; set; }
        public virtual ProductDTO Product { get; set; }
        public int Quantity { get; set; }

    }
}
