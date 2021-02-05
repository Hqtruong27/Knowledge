using AutoMapper;
using Knowledge.Common.Helper;
using Knowledge.Web.IdentityProvider.Data.EF;
using Knowledge.Web.IdentityProvider.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Knowledge.Web.IdentityProvider.Bussiness
{
    public interface IRoleManager : IBaseCoreManager<string, RoleResponse, RoleRequest>
    {
        abstract Task<Pagination> GetPaginationAsync(string q, int offset, int limit);
    }
    public class RoleManager : BaseCoreManager, IRoleManager
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public RoleManager(IdentityProviderDbContext context, RoleManager<IdentityRole> roleManager, IMapper mapper) : base(context, mapper)
        {
            _roleManager = roleManager;
        }
        public async Task<RoleResponse> CreateAsync(RoleRequest request)
        {
            try
            {
                var role = _mapper.Map<IdentityRole>(request);
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                    return await Task.FromResult(_mapper.Map<RoleResponse>(result));
                throw new Exception($"Cannot create role: {request.ToJsonString()}");
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
                throw new Exception($"Cannot create role: {request.ToJsonString()}", ex);
            }
        }
        public async Task<Pagination> GetPaginationAsync(string q, int offset, int limit)
        {
            q = q.Trim().ToLower();
            var result = await _context.Roles.Where(r => string.IsNullOrWhiteSpace(q) || r.Id.ToLower().Contains(q) || r.Name.ToLower().Contains(q)).AsNoTracking().ToArrayAsync();
            var items = result.Skip((offset - 1) * limit).Take(offset).Select(item => _mapper.Map<RoleResponse>(item));
            var pagination = new Pagination() { Items = items.ToJsonString(), TotalRecords = result.Length };
            return pagination;
        }
        public async Task DeleteAsync(string id)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                var result = await _roleManager.DeleteAsync(role);
                if (result.Succeeded)
                    _mapper.Map<RoleResponse>(role);
                throw new Exception($"Cannot find role with id: {id}");
            }
            catch (ArgumentNullException ex)
            {
                await Task.FromException(ex);
                throw new Exception($"Cannot find role with id: {id}", ex);
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
                throw new Exception($"Cannot delete role with id: {id}", ex);
            }
        }

        public async Task<RoleResponse> FindByIdAsync(string id)
        {
            var result = await _roleManager.FindByIdAsync(id);
            return _mapper.Map<RoleResponse>(result);
        }

        public virtual async Task<IQueryable<RoleResponse>> GetsAsync()
        {
            var roles = _context.Roles.Select(role => _mapper.Map<RoleResponse>(role));
            return await Task.FromResult(roles);
        }
        public async Task<RoleResponse> UpdateAsync(string id, RoleRequest request)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(id);
                role = _mapper.Map(request, role);
                var result = await _roleManager.UpdateAsync(role);
                if (result.Succeeded)
                    return _mapper.Map<RoleResponse>(result);
                throw new Exception($"Cannot update role: {request.ToJsonString()}");
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
                throw new Exception($"Cannot update role: {request.ToJsonString()}", ex);
            }
        }
    }
}
