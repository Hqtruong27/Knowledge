using Knowledge.Data.EF;
using Knowledge.Data.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading.Tasks;

namespace Knowledge.Data.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository CategoryRepository { get; }
        Task BeginTransaction();
        Task Commit();
        Task Rollback();
        Task SaveChange();
        //ApplicationDbContext Context { get; }
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction Transaction { get; set; }

        private readonly IHttpContextAccessor _httpContextAccessor;
        public ICategoryRepository CategoryRepository { get; set; }

        private static readonly object instance = new object();
        public UnitOfWork(IDbContext<ApplicationDbContext> dbContext, IHttpContextAccessor httpContextAccessor,
                          ICategoryRepository categoryRepository)
        {
            if (_context == null)
            {
                lock (instance)
                {
                    _context = dbContext.GetContext();
                }
            }
            _httpContextAccessor = httpContextAccessor;
            CategoryRepository = categoryRepository;
        }

        public async Task BeginTransaction() => Transaction = await _context.Database.BeginTransactionAsync();

        public async Task Commit() => await Transaction.CommitAsync();

        public async Task Rollback() => await Transaction.RollbackAsync();

        public async Task SaveChange() => await _context.SaveChangesAsync();

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}