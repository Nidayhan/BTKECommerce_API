using BTKECommerce_domain.Data;
using BTKECommerce_domain.Entities;
using BTKECommerce_Domain.Interfaces;
using BTKECommerce_Infrastructure.Repository;

namespace BTKECommerce_Infrastructure.UoW
{
    internal class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _context;
        public IGenericRepository<Category> Categories { get; }

        public IGenericRepository<Product> Products { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Categories = new GenericRepository<Category>(context);
            Products = new GenericRepository<Product>(context);
        }
        public async Task<int> SaveChangesAsync()
        {
            return await _context .SaveChangesAsync();
        }
        
        public void Dispose()
        {
            _context.Dispose();
        } 
    }

          
}
