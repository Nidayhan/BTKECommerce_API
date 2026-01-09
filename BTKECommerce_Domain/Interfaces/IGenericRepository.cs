using BTKECommerce_Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Query;

namespace BTKECommerce_Domain.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        //Create
        void Add(T entity);
        //Delete
        void Delete(T entity);
        //Update
        //GetAll
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllAsyncWithInclude(Func<IQueryable<T>,IIncludableQueryable<T,object>> includeExpressions);
        //Get
        Task<T> GetById(Guid Id);
        T Update(T entity);
    }
}
