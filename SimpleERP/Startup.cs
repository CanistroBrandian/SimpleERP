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
using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.Auth;
using SimpleERP.Data.Repository;
using SimpleERP.Helpers;
using SimpleERP.Identity;
using System;
using System.IO;
using System.Reflection;
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

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Name = "SimpleERP.Auth";
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(7); // - 7 days "Remember me"
                options.LoginPath = "/Account/Login/";
                options.AccessDeniedPath = "/";
            })
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = AuthHelper.BuildTokenValidationParameters();
            });

            InitializeApplicationServices(services);
            services.AddScoped<IUserClaimsPrincipalFactory<User>, ERPUserClaimsPrincipalFactory>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "My API", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

          
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider service)
        {
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

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
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            //AddSupervisor(service).Wait();
        }

        protected virtual void ConfigureDbContext(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("SimpleERPContextConnection");
            services.AddDbContext<ContextEF>(options => options.UseSqlServer(connection)
                                                               .ConfigureWarnings(w => w.Throw(RelationalEventId.QueryClientEvaluationWarning)));
        }


        protected virtual void InitializeApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IEmployeRepository, EmployeRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IDepartamentRepository, DepartamentRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGoalRepository, GoalRepository>();
            services.AddScoped<IWarehouseRepository, WarehouseRepository>();
            services.AddScoped<IManagerRepository, ManagerRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();
        }

        public async Task AddSupervisor(IServiceProvider serviceProvider)
        {
            var _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var _userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            string SupervisorRole = "Supervisor";

            var roleCheck = await _roleManager.RoleExistsAsync(SupervisorRole);
            if (!roleCheck)
            {
                //create the roles and seed them to the database  
                await _roleManager.CreateAsync(new IdentityRole(SupervisorRole));
            }
            var admin = await _userManager.FindByEmailAsync("admin@mail.ru");
            if (admin == null)
            {
                var newAdmin = new User
                {
                    NameFirst = "admin",
                    NameLast = "admin",
                    Phone = "37529144",
                    Adress = "adress",
                    Email = "admin@mail.ru",
                    IsActive = true,
                    UserName = "admin@mail.ru",

                };
                string pass = "looser";
                var result = await _userManager.CreateAsync(newAdmin, pass);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newAdmin, SupervisorRole);
                }
            }
        }
    }
}
