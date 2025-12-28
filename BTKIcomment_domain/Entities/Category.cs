using BTKECommerce_domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_domain.Entities
{
    public class Category : BaseEntity
    {

        public string CategoryName { get; set; }

        public String Description { get; set; } 

        public ICollection<Product> Products { get; set; } //navigation proporty
    }
}
