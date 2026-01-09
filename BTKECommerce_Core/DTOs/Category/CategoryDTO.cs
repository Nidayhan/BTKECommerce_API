using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_Core.DTOs.Category
{
    public class CategoryDTO
    {
        public string CategoryName { get; set; }

        public string Description { get; set; }
        public string? CreatedBy { get; set; }
    }
}
