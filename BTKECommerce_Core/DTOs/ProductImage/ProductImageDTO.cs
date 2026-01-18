using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_Core.DTOs.ProductImage
{
    public class ProductImageDTO
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
        public class AddProductImageDTO
        {
            public IFormFile Image { get; set; } = null!;

        }
    }
}