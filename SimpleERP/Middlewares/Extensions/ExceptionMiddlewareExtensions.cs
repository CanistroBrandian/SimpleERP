using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;

namespace SimpleERP.Middlewares.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        ILogger<IExceptionHandlerFeature> logger = (ILogger<IExceptionHandlerFeature>)app.ApplicationServices.GetService(typeof(ILogger<IExceptionHandlerFeature>));
                        logger.LogError($"Error: {contextFeature.Error}");

                        var env = (IHostingEnvironment)app.ApplicationServices.GetService(typeof(IHostingEnvironment));
                        if (env.IsProduction())
                        {
                            // send email here
                        }

                        await context.Response.WriteAsync(new
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error."
                        }.ToString());
                    }
                });
            });
        }
    }
}