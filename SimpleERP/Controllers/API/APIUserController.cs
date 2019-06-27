using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.Attributes;
using SimpleERP.Data.Entities.Auth;
using SimpleERP.Helpers;
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
            return Ok();
        }

    }
}