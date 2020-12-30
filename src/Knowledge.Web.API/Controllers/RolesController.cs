using AutoMapper;
using Knowledge.Data.UOW;
using Knowledge.Services.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Knowledge.Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public RolesController(RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        //URl: GET: http://localhost:5001/api/roles
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await _unitOfWork.RoleRepository.GetAll();
            var roles = result.Select(x => _mapper.Map<RoleViewModel>(x));
            return Ok(roles);
        }

        //URl: GET: http://localhost:5001/api/roles?q={q}&offset=2&limit=10
        [HttpGet("pagination")]
        public async Task<IActionResult> GetPagination(string q, int offset, int limit)
        {
            var strQ = await _unitOfWork.RoleRepository.GetAll();
            if (!string.IsNullOrEmpty(q))
            {
                strQ = strQ.Where(x => x.Id.Contains(q) || x.Name.Contains(q));
            }
            var totalRecords = await strQ.CountAsync();
            var items = await strQ.Skip((offset - 1) * limit).Take(offset)
                                  .Select(x => _mapper.Map<RoleViewModel>(x)).ToListAsync();
            var pagination = new Pagination<RoleViewModel>() { Items = items, TotalRecords = totalRecords };
            return Ok(pagination);
        }

        //URl: GET: http://localhost:5001/api/roles/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            return Ok(new RoleViewModel { Id = role.Id, Name = role.Name });
        }
        //URl: POST: http://localhost:5001/api/roles
        [HttpPost]
        public async Task<IActionResult> CreateAsync(RoleViewModel model)
        {
            var role = new IdentityRole() { Id = model.Id, Name = model.Name, NormalizedName = model.Name.ToUpper() };
            var result = await _roleManager.CreateAsync(role);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = role.Id }, model);
        }

        //URl: PUT: http://localhost:5001/api/roles/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] RoleViewModel model)
        {
            if (id != model.Id) return BadRequest();

            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();
            role.Name = model.Name;
            role.NormalizedName = model.Name.ToUpper();
            var result = await _roleManager.UpdateAsync(role);
            if (!result.Succeeded) return BadRequest(result.Errors);
            return NoContent();
        }
        //URl: DELETE: http://localhost:5001/api/roles/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null) return NotFound();

            var result = await _roleManager.DeleteAsync(role);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(_mapper.Map<RoleViewModel>(role));
        }
    }
}
