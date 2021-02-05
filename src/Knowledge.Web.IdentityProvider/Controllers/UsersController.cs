using static System.Net.HttpStatusCode;
using static Knowledge.Web.IdentityProvider.Common.UserErr;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Knowledge.Web.IdentityProvider.Controllers;
using Knowledge.Web.IdentityProvider.Data;
using Knowledge.Web.IdentityProvider.Bussiness;
using Knowledge.Web.IdentityProvider.Models;
using Knowledge.Web.IdentityProvider.Common;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Knowledge.Web.API.Controllers
{
    public class UsersController : BaseController
    {
        private readonly IUserManager UserManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger _logger;

        public UsersController(IUserManager userManager, ILogger<UsersController> logger, SignInManager<User> signInManager)
        {
            UserManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        // GET: api/<UsersController>/users
        [HttpGet("users")]
        public async Task<IActionResult> Get()
        {
            _logger.LogWarning("This is Warning");
            //_Logger.LogError("This is LogError");
            //_Logger.LogCritical("This is LogCritical");
            _logger.LogDebug("This is LogDebug");
            //_Logger.LogTrace("This is LogTrace");
            var signIn = _signInManager.IsSignedIn(User);
            if (signIn)
            {

            }
            var users = await UserManager.GetsAsync();
            //var x = new { Id = "xxxx", Name = "yyyy", Price = "zzz" };
            //Log.Information("xCreated {@User} on {Created}", x, DateTime.Now);
            _logger.LogInformation("Created {@User} on {Created}", users, DateTime.Now);
            return await users.AnyAsync() ? Ok(users) : NotFound(GETS404.GetName());
        }

        // GET api/<UsersController>/user/id
        //[HttpGet("user/{id}.{format}?")]
        [HttpGet("user/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var user = await UserManager.FindByIdAsync(id);
            return user == null ? NotFound(GET404.GetName()) : Ok(user);
        }

        // POST api/<UsersController>/create/id
        [HttpPost("user/create")]
        public async Task<IActionResult> Post([FromBody] UserRequest request)
        {
            try
            {
                var user = await UserManager.CreateAsync(request);
                return CreatedAtAction(OK, nameof(Get), user);
            }
            catch (ArgumentNullException ex)
            {
                throw new Exception($"Failed to Create User!", ex);
            }
            catch (Exception)
            {
                return StatusCode(InternalServerError, POST500.GetName());
            }
        }

        // PUT api/<UsersController>/update/id
        [HttpPut("user/update/{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] UserRequest request)
        {
            try
            {
                if (id != request.Id)
                    return BadRequest(PUT400.GetName());

                var user = await UserManager.UpdateAsync(id, request);
                return Ok(user);
            }
            catch (Exception)
            {
                return StatusCode(InternalServerError, PUT500.GetName());
            }
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("user/delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await UserManager.DeleteAsync(id);
                return Ok($"Deleted User Id: {id}");
            }
            catch (Exception)
            {
                return StatusCode(InternalServerError, POST500.GetName());
            }
        }

        #region PRIVATE

        #endregion PRIVATE
    }
}