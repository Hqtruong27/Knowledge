using AutoMapper;
using Knowledge.Data.UOW;
using System.Linq;
using System.Threading.Tasks;

namespace Knowledge.Infrastructure.Services
{
    public interface IBaseCoreService<T, TResponse, TRequest>
    {
        abstract Task<IQueryable<TResponse>> GetsAsync();
        abstract Task<TResponse> FindByIdAsync(T id);
        abstract Task<TResponse> CreateAsync(TRequest request);
        abstract Task<TResponse> UpdateAsync(T id, TRequest request);
        abstract Task DeleteAsync(T id);
    }
    public class BaseCoreService
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        public BaseCoreService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
