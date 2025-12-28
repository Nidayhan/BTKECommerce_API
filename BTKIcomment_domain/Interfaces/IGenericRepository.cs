using BTKECommerce_domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {

        //Create
        void Add(T entity);

        //Delete
        void Delete(T entity);

        //Update
        Task<T> Update(T entity);

        //GetAll
        Task<IEnumerable<T>> GelAll();

        //Get
        Task<T> GetById(Guid Id);
    }
}
