using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleERP.Data.Entities.Auth;
using SimpleERP.Helpers;
using SimpleERP.Models.API.Auth;
using SimpleERP.Models.ViewModels;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleERP.Controllers.API
{
    [Route("api/auth")]
    [ApiController]
    public class APIAuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<User> _signInManager;

        public APIAuthController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }
        /// <summary>
        /// User authorization
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /login
        ///     {
        ///        "userName": "admin@mail.ru",
        ///        "password": "looser"       
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns> Generates JWT and validates it with the data entered</returns>
        /// <response code="200">Returns the JWT and username</response>
        /// <response code="400">Invalid username or password or user is not active</response>            
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> TokenAsync(LoginModel model)
        {
            var username = model.UserName;
            var password = model.Password;

            var identity = await GetIdentityAsync(username, password);

            if (identity == null)
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
        /// <summary>
        /// Registration new user
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///   POST /register
        ///     {   
        ///			   "NameFirst": "NameFirst",
        ///            "NameLast": "NameLast",
        ///            "Phone": "375291488",
        ///            "Adress": "adres",
        ///            "UserName": "User",
        ///            "Email": "client@mail.ru",
        ///            "password": "client",
        ///            "passwordConfirm": "looser",
        ///            "type": "Client"
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns> Generates JWT and validates it with the data entered</returns>
        /// <response code="200">Returns the generated JWT and the login of the registered user</response>
        /// <response code="400">If not valid date or Email is taken</response>                     
        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
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
            var pass = model.Password;
            var passConfirm = model.PasswordConfirm;
            if (pass.Equals(passConfirm))
            {
                var result = await _userManager.CreateAsync(user, pass);

                if (result.Succeeded)
                {
                    var identity = await GetIdentityAsync(user.UserName, model.Password);
                    if (identity == null)
                    {
                        return BadRequest("User is not active");
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
            else
                return BadRequest("Password and PasswordConfirm dont equal");
        }



        private async Task<ClaimsIdentity> GetIdentityAsync(string username, string password)
        {
            User user = await _userManager.FindByNameAsync(username);
            if (user == null)// || user not active
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