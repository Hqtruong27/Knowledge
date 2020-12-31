using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Knowledge.Web.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index1()
        {
            return View();
        }
    }
}
