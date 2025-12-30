using BTKECommerce_domain.Entities;
using BTKECommerce_Domain.Interfaces;

namespace BTKECommerce_Infrastructure.UoW
{
    public interface IUnitOfWork
    {
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Product> Products { get; }
        Task<int> SaveChangesAsync();
    }
}
