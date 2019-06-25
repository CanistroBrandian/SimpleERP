using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.Extensions;
using SimpleERP.Helpers;
using SimpleERP.Models.Entities.Auth;

namespace SimpleERP.Controllers
{
    public class ProtectedController : Controller
    {
        [Authorize]
        public IActionResult GetUserName()
        {
            return Ok($"User name is {User.Identity.Name}");
        }

        [Authorize]
        public IActionResult GetUserRoles()
        {
            return Ok($"User roles are {User.GetRoles()}");
        }

        [Authorize(Roles = nameof(Employe))]
        public IActionResult EmployeCheck()
        {
            return Ok($"This is available only for Employee");
        }

        [Authorize(Roles = nameof(Client))]
        public IActionResult ClientCheck()
        {
            return Ok($"This is available only for Client");
        }

        [Authorize(Roles = nameof(Manager))]
        public IActionResult ManagerCheck()
        {
            return Ok($"This is available only for Manager");
        }

        [Authorize(Roles = AuthHelper.SUPERVISOR_ROLE)]
        public IActionResult SupervisorCheck()
        {
            return Ok($"This is available only for Supervisor");
        }
    }
}