using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleERP.Models.Context;

namespace SimpleERP.Tests
{
    public class TestStartup : Startup
    {
        public TestStartup(IConfiguration configuration, IHostingEnvironment env) : base(configuration, env)
        {
        }

        protected override void ConfigureDbContext(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("SQLLite");           
            services.AddEntityFrameworkSqlite()
                    .AddDbContext<ContextEF>(
                          options => options.UseSqlite(connection)
                                            .ConfigureWarnings(w => w.Throw(RelationalEventId.QueryClientEvaluationWarning))
                     );
        }
    }
}
