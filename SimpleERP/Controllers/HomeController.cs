using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleERP.Data.Context;
using System;

namespace SimpleERP.Controllers
{
    public class HomeController : Controller
    {

        private readonly ContextEF _context;

        public HomeController(ContextEF context, ILogger<HomeController> logger)
        {
            _context = context;
            logger.LogInformation("New Visitor");
            throw new Exception("Error that should be logged");
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
