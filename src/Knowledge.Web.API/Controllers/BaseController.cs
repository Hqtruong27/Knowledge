using static System.Net.HttpStatusCode;
using Microsoft.AspNetCore.Mvc;
using Knowledge.Infrastructure.Models.Response;
using System.Net;
using Knowledge.Common.Helper;

namespace Knowledge.Web.API.Controllers
{
    [ApiController]
    [Route("api")]
    //[Authorize]
    //[Authorize(Policy = Constants.Bearer)]
    public class BaseController : ControllerBase
    {
        [NonAction]
        public virtual OkObjectResult Succeeded(object value = null, string url = null)
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
            return StatusCode(statusCode.ToInt(), new ResponseResult { StatusCode = statusCode, Value = value, Message = message });
        }
    }

}
