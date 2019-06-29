using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using SimpleERP.Data.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleERP.Identity
{
    public class ERPUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User>
    {

        public ERPUserClaimsPrincipalFactory(UserManager<User> userManager, IOptions<IdentityOptions> optionsAccessor) : base(userManager, optionsAccessor)
        {
          
        }

        public override async Task<ClaimsPrincipal> CreateAsync(User user)
        {
            
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var identity = await base.GenerateClaimsAsync(user);

            IList<string> roles = await UserManager.GetRolesAsync(user);

            if (user is Manager)
            {
                roles.Add(nameof(Manager));
            }
            else if (user is Employe)
            {
                roles.Add(nameof(Employe));
            }
            else if (user is Client)
            {
                roles.Add(nameof(Client));
            }
            if (roles.Any())
            {
                identity.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, string.Join(",", roles)));
            }
            //else if (user is Client)
            //{
            //    roles.Add(nameof(Client));
            //}

            return new ClaimsPrincipal(identity);
        }
    }
}
