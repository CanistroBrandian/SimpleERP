using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.Auth;
using SimpleERP.Models.API.User;
using SimpleERP.Models.ViewModels;

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