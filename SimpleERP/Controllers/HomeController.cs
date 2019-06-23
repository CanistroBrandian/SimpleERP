using Microsoft.AspNetCore.Mvc;
using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities;
using SimpleERP.Models.Entities.Auth;
using SimpleERP.Models.Entities.GoalEntity;
using SimpleERP.Models.Entities.WarehouseEntity;
using System;

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
            Employe employe = new Employe
            {
                Id = 2,
                NameFirst = "Bob",
                NameLast = "Niohert",
                Phone = "375214885",
                Adress = "ul.malinia",
                Login = "Login",
                Password = "Pass",
                DepartamentId = 8
            };

            Manager manager = new Manager
            {
                NameFirst = "Bob",
                NameLast = "Niohert",
                Phone = "375214885",
                Adress = "ul.malinia",
                Login = "Login",
                Password = "Pass",
                DepartamentId = 8
            };
            User user = new User
            {
                NameFirst = "Bob",
                NameLast = "Niohert",
                Phone = "375214885",
                Adress = "ul.malinia",
                Login = "Login",
                Password = "Pass",

            };


            Goal goal = new Goal
            {
                Name = "we will win",
                Description = "lba bla bal",
                DateCreated = new DateTime(1018, 10, 15),
                DateFinished = new DateTime(1018, 10, 15),
                AssigneId = 4,
                ReporterId = 3
            };

            _context.Goals.Add(goal);

            //   _context.Managers.Add(manager);
            //    _context.Users.Add(user);
            _context.SaveChanges();
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }


    }
}
