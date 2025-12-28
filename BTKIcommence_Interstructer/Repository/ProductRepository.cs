using BTKECommerce_domain.Data;
using BTKECommerce_domain.Entities;
using BTKECommerce_domain.Interfaces;

namespace BTKECommerce_Infrastructure.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
