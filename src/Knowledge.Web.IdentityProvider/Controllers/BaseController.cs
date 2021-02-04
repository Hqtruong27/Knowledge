using Knowledge.Web.IdentityProvider.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static System.Net.HttpStatusCode;

namespace Knowledge.Web.IdentityProvider.Controllers
{
    [ApiController]
    [Route("api")]
    //[Authorize]
    //[Authorize(Policy = Constants.Bearer)]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public virtual OkObjectResult Ok(object value = null, string url = null)
        {
            if (value is null)
            {
                return Ok(new ResponseResult { StatusCode = OK, RedirectUrl = url });
            }
            return Ok(new ResponseResult { StatusCode = OK, Value = value, RedirectUrl = url });
        }
        [NonAction]
        public virtual NotFoundObjectResult NotFound(string message, object value = null)
        {
            return NotFound(new ResponseResult { StatusCode = HttpStatusCode.NotFound, Message = message, Value = value });
        }
        [NonAction]
        public virtual BadRequestObjectResult BadRequest(string message, object value = null)
        {
            return BadRequest(new ResponseResult { StatusCode = HttpStatusCode.NotFound, Message = message, Value = value });
        }
        [NonAction]
        public virtual CreatedAtActionResult CreatedAtAction(HttpStatusCode statusCode, string uri, object value)
        {
            return CreatedAtAction(uri, new ResponseResult { StatusCode = statusCode, Value = value });
        }
        [NonAction]
        public virtual ObjectResult StatusCode(HttpStatusCode statusCode, object value = null, string message = null)
        {
            return StatusCode((int)statusCode, new ResponseResult { StatusCode = statusCode, Value = value, Message = message });
        }
    }

}
