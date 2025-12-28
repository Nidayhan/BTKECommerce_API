using BTKECommerce_domain.Data;
using BTKECommerce_domain.Entities.Base;
using BTKECommerce_domain.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            _context.SaveChanges();

        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public Task<IEnumerable<T>> GelAll()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetById(Guid Id)
        {
            var query = _dbSet.AsQueryable();
            return await query.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
            return entity;
        }

        Task<T> IGenericRepository<T>.Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}