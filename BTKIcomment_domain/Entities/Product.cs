using BTKECommerce_domain.Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;

namespace BTKECommerce_domain.Entities
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
}
