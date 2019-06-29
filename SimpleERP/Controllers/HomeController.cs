using Microsoft.AspNetCore.Mvc;
using SimpleERP.Data.Context;

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
