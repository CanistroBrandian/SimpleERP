using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Events;
using System;

namespace SimpleERP.Extensions
{
    public static class WebHostExtensions
    {
        public static IWebHost ConfigureSerilogLogging(this IWebHost webHost)
        {
            Func<LogEvent, bool> isError = s => s.Level == LogEventLevel.Error || s.Level == LogEventLevel.Fatal;
            var configuration = (IConfiguration)webHost.Services.GetService(typeof(IConfiguration));
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .Enrich.FromLogContext()
                .WriteTo.Logger(lc =>
                {
                    lc.Filter.ByIncludingOnly(isError)
                      .WriteTo.RollingFile("logs/errors-{Date}.txt");
                })
                .WriteTo.Logger(lc =>
                {
                    lc.Filter.ByExcluding(isError)
                      .WriteTo.RollingFile("logs/log-{Date}.txt");

                })
                .WriteTo.Console()
                .CreateLogger();

            return webHost;
        }
    }
}