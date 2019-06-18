using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleERP.Areas.Identity.Data;
using SimpleERP.Models;

[assembly: HostingStartup(typeof(SimpleERP.Areas.Identity.IdentityHostingStartup))]
namespace SimpleERP.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SimpleERPContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SimpleERPContextConnection")));

                services.AddDefaultIdentity<SimpleERPUser>()
                    .AddEntityFrameworkStores<SimpleERPContext>();
            });
        }
    }
}