using BTKECommerce_Domain.Entities.Base;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

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
        Task<IEnumerable<T>> GetAllAsyncExpression( Expression<Func<T, bool>>
            predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includeExpressions, bool asNoTracking = false
            );
        Task<T> GetById(Guid Id);
        T Update(T entity);
    }
}
