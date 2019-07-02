using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.Attributes;
using SimpleERP.Data.Entities.Auth;
using SimpleERP.Helpers;
using SimpleERP.Models.API.Employe;
using SimpleERP.Models.API.User;
using System.Threading.Tasks;

namespace SimpleERP.Controllers.API
{
    [Route("api/user")]
    [ApiController]
    public class APIUserController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        public APIUserController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Update product by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /active
        ///     {
        ///        "email":"employe@mail.ru",
        ///        "isActive" : true
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>Changes field isActive </returns>
        /// <response code="200">Return model of user</response>
        /// <response code="404">Not found username or invalid data</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [APIAuthorize(Roles = AuthHelper.SUPERVISOR_ROLE)]
        [HttpPut("activate")]
        public async Task<ActionResult> ChangeActive(ChangeActivationModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByEmailAsync(model.Email);

            user.IsActive = model.IsActive.Value;
            await _userManager.UpdateAsync(user);
            return Ok(user);
        }
    }
}
