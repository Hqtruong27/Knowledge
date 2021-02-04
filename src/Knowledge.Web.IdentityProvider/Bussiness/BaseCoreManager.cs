using AutoMapper;
using Knowledge.Web.IdentityProvider.Data.EF;
using System.Linq;
using System.Threading.Tasks;

namespace Knowledge.Web.IdentityProvider.Bussiness
{
    public interface IBaseCoreManager<T, TResponse, TRequest>
    {
        abstract Task<IQueryable<TResponse>> GetsAsync();
        abstract Task<TResponse> FindByIdAsync(T id);
        abstract Task<TResponse> CreateAsync(TRequest request);
        abstract Task<TResponse> UpdateAsync(T id, TRequest request);
        abstract Task DeleteAsync(T id);
    }
    public class BaseCoreManager
    {
        protected readonly IdentityProviderDbContext _context;
        protected readonly IMapper _mapper;
        public BaseCoreManager(IdentityProviderDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
    }
}
