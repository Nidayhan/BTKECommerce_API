


using BTKECommerce_Domain.Entities.Base;

namespace BTKECommerce_Domain.Entities
{
    public class Category : BaseEntity
    {

        public string CategoryName { get; set; } 

        public string Description { get; set; }

        public string CreatedBy { get; set; }
        //Navigation Property
        public ICollection<Product> Products { get; set; }
    }
}
