using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleERP.Helpers;
using SimpleERP.Data.Entities.Auth;
using SimpleERP.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleERP.Controllers.API
{
    [Route("api/auth")]
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
       
        [HttpPost("login")]
        public async Task<IActionResult> TokenAsync()
        {
            var username = Request.Form["username"];
            var password = Request.Form["password"];

            var identity = await GetIdentityAsync(username, password);
            
            if (identity == null )
            {
                return BadRequest("Invalid username or password or user is not active.");
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

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.Values.FirstOrDefault()?.Errors.FirstOrDefault()?.ErrorMessage);
            };
            User user = null;
            if (model.Type == nameof(Manager))
            {
                user = new Manager
                {
                    DepartamentId = model.DepartmentId.Value
                };

            }
            else if (model.Type == nameof(Employe))
            {
                user = new Employe
                {
                    DepartamentId = model.DepartmentId.Value
                };
            }
            else if (model.Type == nameof(Client))
            {
                user = new Client();
            }

            user.NameFirst = model.NameFirst;
            user.NameLast = model.NameLast;
            user.Phone = model.Phone;
            user.Adress = model.Adress;
            user.UserName = model.Email;
            user.Email = model.Email;
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                var identity = await GetIdentityAsync(user.UserName, model.Password);
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
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return BadRequest(ModelState.Values.FirstOrDefault()?.Errors.FirstOrDefault()?.ErrorMessage);
        }



        private async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
        {
            User user = await _userManager.FindByNameAsync(username);
            if (user == null || user.IsActive==false)// || user not active
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