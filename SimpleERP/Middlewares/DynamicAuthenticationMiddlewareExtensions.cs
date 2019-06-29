using Microsoft.AspNetCore.Builder;

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
