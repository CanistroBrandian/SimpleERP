using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.Extensions;
using SimpleERP.Helpers;
using SimpleERP.Models.Entities.Auth;

namespace SimpleERP.Controllers
{
    public class ProtectedController : Controller
    {
        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult GetUserName()
        {
            return Ok($"User name is {User.Identity.Name}");
        }

        [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
        public IActionResult GetUserRoles()
        {
            return Ok($"User roles are {User.GetRoles()}");
        }

        [Authorize(
            AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme,
            Roles = nameof(Employe))]
        public IActionResult EmployeCheck()
        {
            return Ok($"This is available only for Employee");
        }

        [Authorize(
            AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme,
            Roles = nameof(Client))]
        public IActionResult ClientCheck()
        {
            return Ok($"This is available only for Client");
        }

        [Authorize(
            AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme,
            Roles = nameof(Manager))]
        public IActionResult ManagerCheck()
        {
            return Ok($"This is available only for Manager");
        }

        [Authorize(
            AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme,
            Roles = AuthHelper.SUPERVISOR_ROLE)]
        public IActionResult SupervisorCheck()
        {
            return Ok($"This is available only for Supervisor");
        }
    }
}