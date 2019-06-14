using Microsoft.AspNetCore.Mvc;
using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using System;

namespace SimpleERP.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployeRepository _repository;
       public HomeController (IEmployeRepository repo)
        {
            _repository = repo;
        }
        public IActionResult Index()
        {
            return View(_repository.GetEmployes());
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
