using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleERP.Helpers;
using SimpleERP.Identity;
using SimpleERP.Middlewares;
using SimpleERP.Models.Abstract;

using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.Auth;
using SimpleERP.Models.Repository;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SimpleERP
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
             .SetBasePath(env.ContentRootPath)
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
             .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public virtual void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = AuthHelper.BuildTokenValidationParameters();
            })
           .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
           {
               options.Cookie.Name = "SimpleERP.Auth";
               options.Cookie.HttpOnly = true;
               options.ExpireTimeSpan = TimeSpan.FromDays(7); // - 7 days "Remember me"
               options.LoginPath = "/Account/Login/";
               options.AccessDeniedPath = "/";
           });

            ConfigureDbContext(services);

            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 5;   // минимальная длина
                opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
                opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
                opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
                opts.Password.RequireDigit = false; // требуются ли цифры
            }).AddEntityFrameworkStores<ContextEF>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            InitializeApplicationServices(services);

            services.AddScoped<IUserClaimsPrincipalFactory<User>, ERPUserClaimsPrincipalFactory>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.AddDynamicSchemeAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        protected virtual void ConfigureDbContext(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("SimpleERPContextConnection");
            services.AddDbContext<ContextEF>(options => options.UseSqlServer(connection)
                                                               .ConfigureWarnings(w => w.Throw(RelationalEventId.QueryClientEvaluationWarning)));
        }


        protected virtual void InitializeApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
        }
    }
}
