using BTKECommerce_Domain.Data;
using BTKECommerce_Domain.Entities.Base;
using BTKECommerce_Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace BTKECommerce_Infrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
            
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsyncWithInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> includeExpressions)
        {
            IQueryable<T> query = _dbSet.AsQueryable();
            if(includeExpressions != null)
            {
                query = includeExpressions(query);
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetById(Guid Id)
        {
            var query = _dbSet.AsQueryable();
            return await query.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            return entity;
        }
    }
}
