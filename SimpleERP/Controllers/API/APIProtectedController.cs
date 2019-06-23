using Microsoft.AspNetCore.Mvc;
using SimpleERP.Attributes;
using SimpleERP.Extensions;
using SimpleERP.Helpers;
using SimpleERP.Models.Entities.Auth;

namespace SimpleERP.Controllers.API
{
    [Route("api/protected")]
    [ApiController]
    public class APIProtectedController : ControllerBase
    {
        [APIAuthorize]
        [HttpGet("username")]
        public IActionResult GetUserName()
        {
            return Ok($"User name is {User.Identity.Name}");
        }

        [APIAuthorize]
        [HttpGet("userroles")]
        public IActionResult GetUserRoles()
        {
            return Ok(User.GetRoles());
        }

        [APIAuthorize(Roles = nameof(Employe))]
        public IActionResult EmployeCheck()
        {
            return Ok($"This is available only for Employee");
        }

        [APIAuthorize(Roles = nameof(Client))]
        public IActionResult ClientCheck()
        {
            return Ok($"This is available only for Client");
        }

        [APIAuthorize(Roles = nameof(Manager))]
        public IActionResult ManagerCheck()
        {
            return Ok($"This is available only for Manager");
        }

        [APIAuthorize(Roles = AuthHelper.SUPERVISOR_ROLE)]
        public IActionResult SupervisorCheck()
        {
            return Ok($"This is available only for Supervisor");
        }
    }
}