using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleERP.Helpers;
using SimpleERP.Models.Entities.Auth;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleERP.Controllers.API
{
    [Route("api/token")]
    [ApiController]
    public class APITokenController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public APITokenController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> TokenAsync()
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];

            var identity = await GetIdentityAsync(username, password);
            if (identity == null)
            {
                return BadRequest("Invalid username or password.");
            }

            var jwt = AuthHelper.GetJWT(identity);
            var encodedJwt = AuthHelper.EncodeJWT(jwt);

            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return Ok(response);
        }

        private async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
        {
            User user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return null;
            }
            var passwordVerifyResult = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, password);
            if (passwordVerifyResult != PasswordVerificationResult.Success)
            {
                return null;
            }

            var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
        
            return claimsPrincipal?.Identity as ClaimsIdentity;
        }

    }
}