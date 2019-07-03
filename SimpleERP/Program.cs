using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Serilog;
using SimpleERP.Extensions;

namespace SimpleERP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webHost = CreateWebHostBuilder(args).Build();
            webHost.ConfigureSerilogLogging();
            webHost.Run();

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseSerilog()
                .UseIISIntegration();
    }
}
