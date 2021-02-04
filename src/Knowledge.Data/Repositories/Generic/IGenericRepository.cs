using EFCore.BulkExtensions;
using Knowledge.Data.EF;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Knowledge.Data.Repositories.Generic
{
    public interface IReadOnlyRepository<T> where T : class
    {
        Task<IQueryable<T>> ReadsAsync(string include = "");
        Task<IQueryable<T>> FindBy(Expression<Func<T, bool>> predicate, string include = "");
        Task<T> FindAsync(Expression<Func<T, bool>> predicate, string include = "");
        Task<T> Get(Expression<Func<T, bool>> predicate, string include = "");
        void BulkRead(IList<T> entities);
        Task BulkReadAsync(IList<T> entities);
    }
    public interface IGenericRepository<T> : IReadOnlyRepository<T> where T : class
    {
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T id);
        void BulkInsert(IList<T> entities);
        Task BulkInsertAsync(IList<T> entities);
        void BulkUpdate(IList<T> entities);
        Task BulkUpdateAsync(IList<T> entities);
        void BulkDelete(IList<T> entities);
        Task BulkDeleteAsync(IList<T> entities);
    }

    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly ApplicationDbContext _context;
        protected GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public virtual async Task<IQueryable<T>> ReadsAsync(string include = "")
        {
            var query = _context.Set<T>().AsQueryable().AsNoTracking();
            query = IncludeQuery(query, include).AsExpandableEFCore();
            return await Task.FromResult(query);
        }

        public virtual async Task<T> FindAsync(Expression<Func<T, bool>> predicate, string include = "")
        {
            var result = await _context.Set<T>().FindAsync(predicate);
            if (string.IsNullOrWhiteSpace(include))
            {
                var query = _context.Set<T>().AsQueryable().AsNoTracking();
                result = await IncludeQuery(query, include).AsExpandableEFCore().Where(predicate).FirstOrDefaultAsync();
            }
            return result;
        }
        public virtual async Task<T> Get(Expression<Func<T, bool>> predicate, string include = "")
        {
            var result = _context.Set<T>().AsQueryable().AsNoTracking();
            return await IncludeQuery(result, include).AsExpandableEFCore().Where(predicate).FirstOrDefaultAsync();
        }
        public async virtual Task<IQueryable<T>> FindBy(Expression<Func<T, bool>> predicate, string include = "")
        {
            var query = _context.Set<T>().Where(predicate).AsQueryable<T>().AsNoTracking();
            query = IncludeQuery(query, include).AsExpandableEFCore();
            return await Task.FromResult(query);
        }

        public virtual async Task Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public virtual async Task Update(T entity)
        {
            if (_context.Entry(entity).State == EntityState.Detached)
            {
                _context.Set<T>().Attach(entity);
                _context.Update(entity);
            }
            await Task.FromResult(true);
        }
        public virtual async Task Delete(T id)
        {
            var result = _context.Set<T>().Remove(id);
            if (result.State == EntityState.Deleted)
                await Task.FromResult(result);
        }
        public virtual void BulkRead(IList<T> entities)
        {
            _context.BulkRead(entities);
        }
        public virtual async Task BulkReadAsync(IList<T> entities)
        {
            await _context.BulkReadAsync(entities);
        }
        public virtual void BulkInsert(IList<T> entities)
        {
            _context.BulkInsert(entities);
        }
        public virtual async Task BulkInsertAsync(IList<T> entities)
        {
            await _context.BulkInsertAsync(entities);
        }
        public virtual void BulkUpdate(IList<T> entities)
        {
            _context.BulkInsert(entities);
        }
        public virtual async Task BulkUpdateAsync(IList<T> entities)
        {
            await _context.BulkUpdateAsync(entities);
        }
        public virtual void BulkDelete(IList<T> entities)
        {
            _context.BulkDelete(entities);
        }
        public virtual async Task BulkDeleteAsync(IList<T> entities)
        {
            await _context.BulkDeleteAsync(entities);
        }
        #region PRIVATE
        private static IQueryable<T> IncludeQuery(IQueryable<T> query, string rawInclude)
        {
            if (!string.IsNullOrWhiteSpace(rawInclude))
            {
                var includes = rawInclude.Split(',');
                if (includes != null || includes.Any())
                {
                    foreach (var include in includes)
                    {
                        query = query.Include(include);
                    }
                }
            }
            return query;
        }
        #endregion
    }
}
