using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_Core.DTOs.Product
{
    public class ProductDTO
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        public string ImageUrl { get; set; }
        public int StockAmount { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}
