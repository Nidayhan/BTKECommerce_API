using BTKECommerce_Domain.Entities;
using BTKECommerce_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_Infrastructure.UoW
{
    public interface IUnitOfWork
    {
        IGenericRepository<Category> Categories { get; }
        IGenericRepository<Product> Products { get; }
        Task<int> SaveChangesAsync();
    }
}
