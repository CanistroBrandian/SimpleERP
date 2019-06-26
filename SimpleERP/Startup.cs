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
using SimpleERP.Abstract;

using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.Auth;
using SimpleERP.Data.Repository;
using System;
using System.Net;
using System.Threading.Tasks;

namespace SimpleERP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
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

            string connection = Configuration.GetConnectionString("SimpleERPContextConnection");
            services.AddDbContext<ContextEF>(options => options.UseSqlServer(connection)
                                                               .ConfigureWarnings(w => w.Throw(RelationalEventId.QueryClientEvaluationWarning)));
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

           services.AddScoped<IEmployeRepository,EmployeRepository>();
           services.AddScoped<IOrderRepository, OrderRepository>();
           services.AddScoped<IDepartamentRepository, DepartamentRepository>();
           services.AddScoped<IProductRepository, ProductRepository>();
           services.AddScoped<IUserRepository, UserRepository>();
           services.AddScoped<IGoalRepository, GoalRepository>();
           services.AddScoped<IWarehouseRepository, WarehouseRepository>();
           services.AddScoped<IManagerRepository, ManagerRepository>();
           services.AddScoped<IClientRepository, ClientRepository>();
           services.AddScoped<IUserClaimsPrincipalFactory<User>, ERPUserClaimsPrincipalFactory>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
    }
}
