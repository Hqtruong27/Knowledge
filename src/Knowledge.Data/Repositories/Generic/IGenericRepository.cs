using Knowledge.Data.EF;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Knowledge.Data.Repositories.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAll();
        Task<T> Get(string id);
        Task Add(T entity);
        Task Update(T entity);
        void Delete(T id);
    }

    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<T>> GetAll()
        {
            var query = _context.Set<T>().AsQueryable().AsNoTracking();
            return await Task.FromResult(query);
        }

        public async Task<T> Get(string id)
        {
            var result = await _context.Set<T>().FindAsync(id);
            if (result == null) return null;
            return result;
        }

        public async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        public async Task Update(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
                _context.Update(entity);
            }
            await Task.FromResult(true);
        }
        public void Delete(T id)
        {
            _context.Set<T>().Remove(id);
        }
    }
}
