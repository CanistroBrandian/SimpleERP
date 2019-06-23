using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleERP.Middlewares
{
    public static class DynamicAuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder AddDynamicSchemeAuthentication(this IApplicationBuilder app)
        {
            app.UseAuthentication();
            app.UseMiddleware<DynamicAuthenticationMiddleware>();
            return app;
        }
    }
}
