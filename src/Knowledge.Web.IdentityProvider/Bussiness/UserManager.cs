using AutoMapper;
using Knowledge.Common.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Knowledge.Web.IdentityProvider.Models;
using Knowledge.Web.IdentityProvider.Data;
using Knowledge.Web.IdentityProvider.Data.EF;

namespace Knowledge.Web.IdentityProvider.Bussiness
{

    public interface IUserManager : IBaseCoreManager<string, UserResponse, UserRequest>
    {
        abstract Task<Pagination> SearchAsync(string q, int offset, int limit);
    }
    public class UserManager : BaseCoreManager, IUserManager
    {
        private readonly UserManager<User> _userManager;
        public UserManager(IdentityProviderDbContext context, UserManager<User> userManager, IMapper mapper) : base(context, mapper)
        {
            _userManager = userManager;
        }
        public virtual async Task<IQueryable<UserResponse>> GetsAsync()
        {
            var users = _context.Users.AsQueryable().AsNoTracking();
            return await Task.FromResult(users.Select(user => _mapper.Map<UserResponse>(user)));
        }
        public virtual async Task<Pagination> SearchAsync(string q, int offset, int limit)
        {
            var users = _context.Users.Where(h => string.IsNullOrEmpty(q)
                                        || h.Email.Contains(q) || $"{h.FirstName} {h.LastName}".Contains(q)
                                        || h.PhoneNumber.Contains(q) || h.Dob.ToString().Contains(q)).AsEnumerable();
            var items = users.Skip((offset - 1) * limit).Take(limit).Select(item => _mapper.Map<UserResponse>(item));
            var pagination = new Pagination { Items = items, TotalRecords = users.Count() };
            return await Task.FromResult(pagination);
        }

        public virtual async Task<UserResponse> FindByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            return _mapper.Map<UserResponse>(user);
        }

        public virtual async Task<UserResponse> CreateAsync(UserRequest request)
        {
            try
            {
                request.Id = Guid.NewGuid().ToString();
                var user = _mapper.Map<User>(request);
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                    return _mapper.Map<UserResponse>(user);
                throw new Exception($"Faild to Create User: {user.ToJsonString()}");
            }
            catch (Exception ex)
            {
                throw new Exception($"Faild to Create User!", ex);
            }
        }

        public virtual async Task<UserResponse> UpdateAsync(string id, UserRequest request)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                var x = _mapper.Map(request, user);
                var result = await _userManager.UpdateAsync(x);
                if (result.Succeeded)
                    return _mapper.Map<UserResponse>(user);
                throw new Exception($"Faild to Update User with id: {id}");
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
                throw new Exception($"Faild to Update User with id: {id}", ex);
            }
        }

        public virtual async Task DeleteAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                await _userManager.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                await Task.FromException(ex);
                throw new Exception($"Faild to Delete User with id: {id}", ex);
            }
        }
    }
}
