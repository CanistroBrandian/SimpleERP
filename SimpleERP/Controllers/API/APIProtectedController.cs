using Microsoft.AspNetCore.Mvc;
using SimpleERP.Attributes;
using SimpleERP.Data.Entities.Auth;
using SimpleERP.Extensions;
using SimpleERP.Helpers;

namespace SimpleERP.Controllers.API
{
    [Route("api/protected")]
    [ApiController]
    public class APIProtectedController : ControllerBase
    {
        /// <summary>
        /// Get authorized username
        /// </summary>
        /// <returns>Returns Username</returns>
        /// <response code="200">Returns Username</response>        
        /// <response code="404">Not found username</response>        
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [APIAuthorize]
        [HttpGet("username")]
        public IActionResult GetUserName()
        {
            return Ok($"User name is {User.Identity.Name}");
        }

        /// <summary>
        /// Get roles of authorized username
        /// </summary>
        /// <returns>Returns roles Username</returns>
        /// <response code="200">Returns roles Username</response>        
        /// <response code="404">Not found username</response>   
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [APIAuthorize]
        [HttpGet("userroles")]
        public IActionResult GetUserRoles()
        {
            return Ok(User.GetRoles());
        }

        /// <summary>
        /// checks the employee is authorized
        /// </summary>
        /// <returns>Returns message if that employee</returns>
        /// <response code="200">This is available only for Employee</response>        
        /// <response code="404">Not found employee</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [APIAuthorize(Roles = nameof(Employe))]
        [HttpGet("checkemploye")]
        public IActionResult EmployeCheck()
        {
            return Ok($"This is available only for Employee");
        }
        /// <summary>
        /// checks the client is authorized
        /// </summary>
        /// <returns>Returns message if that client</returns>
        /// <response code="200">This is available only for Client</response>        
        /// <response code="404">Not found client</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [APIAuthorize(Roles = nameof(Client))]
        [HttpGet("checkclient")]
        public IActionResult ClientCheck()
        {
            return Ok($"This is available only for Client");
        }
        /// <summary>
        /// checks the manager is authorized
        /// </summary>
        /// <returns>Returns message if that manager</returns>
        /// <response code="200">This is available only for Manager</response>        
        /// <response code="404">Not found manager</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [APIAuthorize(Roles = nameof(Manager))]
        [HttpGet("checkmanager")]
        public IActionResult ManagerCheck()
        {
            return Ok($"This is available only for Manager");
        }
        /// <summary>
        /// checks the supervisor is authorized
        /// </summary>
        /// <returns>Returns message if that supervisor</returns>
        /// <response code="200">This is available only for Supervisor</response>        
        /// <response code="404">Not found supervisor</response>  
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [APIAuthorize(Roles = AuthHelper.SUPERVISOR_ROLE)]
        [HttpGet("checksupervisor")]
        public IActionResult SupervisorCheck()
        {
            return Ok($"This is available only for Supervisor");
        }
    }
}