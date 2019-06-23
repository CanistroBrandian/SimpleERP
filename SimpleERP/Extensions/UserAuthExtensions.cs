using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SimpleERP.Extensions
{
    public static class UserAuthExtensions
    {
        public static string[] GetRoles(this ClaimsPrincipal claimsPrincipal)
        {
            return claimsPrincipal.Claims.FirstOrDefault(s => s.Type == ClaimsIdentity.DefaultRoleClaimType)?.Value.Split(",") ?? Array.Empty<string>();
        }
    }
}
