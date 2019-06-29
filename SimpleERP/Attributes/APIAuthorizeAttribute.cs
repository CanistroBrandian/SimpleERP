using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace SimpleERP.Attributes
{
    public class APIAuthorizeAttribute : AuthorizeAttribute
    {
        public APIAuthorizeAttribute()
        {
            AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
        }
    }
}
