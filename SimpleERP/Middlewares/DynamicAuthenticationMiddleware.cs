using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace SimpleERP.Middlewares
{
    public class DynamicAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public DynamicAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            string scheme = CookieAuthenticationDefaults.AuthenticationScheme;
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                scheme = JwtBearerDefaults.AuthenticationScheme;
            }
            var result = await context.AuthenticateAsync(scheme);
            if (result.Succeeded)
            {
                context.User = result.Principal;
            }
            await _next.Invoke(context);
        }
    }
}
