using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_Core.DTOs.Basket
{
    public class BasketDTO
    {
        public Guid ProductId { get; set; }
        public string UserId { get; set; }

        public int Quantity { get; set; }
    }
}
