using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NIJ.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace NIJ.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            if (statusCode.HasValue)
            {
                if(statusCode == 404 || statusCode == 500)
                {
                    var viewName = $"Error{statusCode.ToString()}";
                    return View(viewName);
                }
            }

            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
