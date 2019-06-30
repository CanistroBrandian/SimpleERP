using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities;
using SimpleERP.Models.Entities.Auth;
using SimpleERP.Models.Entities.GoalEntity;
using SimpleERP.Models.Entities.WarehouseEntity;
using System;
using System.Threading.Tasks;

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
