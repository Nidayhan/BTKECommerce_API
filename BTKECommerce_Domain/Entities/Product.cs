using BTKECommerce_Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BTKECommerce_Domain.Entities
{
    public class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        public string ImageUrl { get; set; }
        public int StockAmount { get; set; }
        [Column(TypeName = "decimal(18,6)")]
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
    }

    /* 
     * 
     * happy coding :)
     * 
     */
}
