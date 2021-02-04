using Microsoft.AspNetCore.Mvc;
using System;
using static System.Net.HttpStatusCode;
using System.Threading.Tasks;
using Knowledge.Web.IdentityProvider.Bussiness;
using Knowledge.Web.IdentityProvider.Models;

namespace Knowledge.Web.IdentityProvider.Controllers
{
    public class RolesController : BaseController
    {
        private readonly IRoleManager RoleManager;

        public RolesController(IRoleManager roleManager)
        {
            RoleManager = roleManager;
        }

        //URl: GET: http://localhost:5001/api/roles
        [HttpGet("roles")]

        public async Task<IActionResult> Gets()
        {
            if (User.Identity.IsAuthenticated)
            {
                var roless = await RoleManager.GetsAsync();
            }
            var roles = await RoleManager.GetsAsync();
            return Ok(roles);
        }

        //URl: GET: http://localhost:5001/api/roles?q={q}&offset=2&limit=10
        [HttpGet("roles/pagination")]
        public async Task<IActionResult> GetPagination(string q, int offset, int limit)
        {
            var pagination = await RoleManager.GetPaginationAsync(q, offset, limit);
            return pagination is null ? NotFound("") : Ok(pagination);
        }

        //URl: GET: http://localhost:5001/api/roles/5
        [HttpGet("role/create/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var role = await RoleManager.FindByIdAsync(id);
            return role == null ? NotFound("") : Ok(role);
        }

        //URl: POST: http://localhost:5001/api/roles
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoleRequest request)
        {
            try
            {
                var result = await RoleManager.CreateAsync(request);
                return result == null ? BadRequest("") : CreatedAtAction(nameof(Get), request);
            }
            catch (Exception)
            {
                return StatusCode(InternalServerError, "");
            }
            //return CreatedAtAction(nameof(GetByIdAsync), new { id = role.Id }, model);
        }

        //URl: PUT: http://localhost:5001/api/roles/{id}
        [HttpPut("role/update/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] RoleRequest request)
        {
            if (id != request.Id) return BadRequest("");
            try
            {
                var result = await RoleManager.UpdateAsync(id, request);
                return result == null ? BadRequest("") : Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(InternalServerError, "");
            }
        }

        //URl: DELETE: http://localhost:5001/api/roles/{id}
        [HttpDelete("role/delete/{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            try
            {
                await RoleManager.DeleteAsync(id);
                return Ok("");
            }
            catch (Exception)
            {
                return StatusCode(InternalServerError, "");
            }
        }
    }
}