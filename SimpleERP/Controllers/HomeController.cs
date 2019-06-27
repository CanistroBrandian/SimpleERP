using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities;
using SimpleERP.Data.Entities.Auth;
using SimpleERP.Data.Entities.GoalEntity;
using SimpleERP.Data.Entities.WarehouseEntity;
using System;
using System.Threading.Tasks;

namespace SimpleERP.Controllers
{
    public class HomeController : Controller
    {

        private readonly ContextEF _context;

        public HomeController(ContextEF context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
