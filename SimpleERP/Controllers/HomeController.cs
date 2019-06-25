using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        private readonly IEmployeOrders _context;

        public HomeController(IEmployeOrders context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


    }
}
