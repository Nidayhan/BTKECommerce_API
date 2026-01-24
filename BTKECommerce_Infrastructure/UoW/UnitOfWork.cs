using BTKECommerce_Domain.Data;
using BTKECommerce_Domain.Entities;
using BTKECommerce_Domain.Interfaces;
using BTKECommerce_Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_Infrastructure.UoW
{
    public class UnitOfWork : IUnitOfWork,IDisposable
    {
        private readonly ApplicationDbContext _context;

        public IGenericRepository<Category> Categories { get; }

        public IGenericRepository<Product> Products { get; }
        public IGenericRepository<ProductImage> ProductImages { get; }

        public IGenericRepository<Basket> Baskets { get; }

        public IGenericRepository<BasketItem> BasketItems { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Categories = new GenericRepository<Category>(context);
            Products = new GenericRepository<Product>(context);
            ProductImages = new GenericRepository<ProductImage>(context);
            Baskets = new GenericRepository<Basket>(context);
            BasketItems = new GenericRepository<BasketItem>(context);
        }


        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
