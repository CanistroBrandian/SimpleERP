using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities;
using SimpleERP.Data.Entities.Auth;
using SimpleERP.Data.Entities.WarehouseEntity;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SimpleERP.Tests.Integration.API
{
    public class APIBaseControllerTest : IDisposable
    {
        private const string PASSWORD = "looser";

        protected readonly TestServer _server;
        protected readonly HttpClient _httpClient;
        

        public APIBaseControllerTest()
        {
            
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<TestStartup>()
                .UseSetting(WebHostDefaults.ApplicationKey, typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            _httpClient = _server.CreateClient();
            CreateDb();
        }
        public void Dispose()
        {
            DeleteDb();
        }

        protected User Supervisor { get; private set; }
        protected Manager Manager { get; private set; }
        protected Employe Employe { get; private set; }
        protected Client Client { get; private set; }

        protected virtual void CreateDb()
        {
            var context = (ContextEF)_server.Host.Services.GetService(typeof(ContextEF));
            context.Database.EnsureCreated();
        }

        protected virtual async Task SeedSupervisor()
        {
            var _roleManager = (RoleManager<IdentityRole>)_server.Host.Services.GetService(typeof(RoleManager<IdentityRole>));
            var _userManager = (UserManager<User>)_server.Host.Services.GetService(typeof(UserManager<User>));

            string SupervisorRole = "Supervisor";
            await _roleManager.CreateAsync(new IdentityRole(SupervisorRole));
            Supervisor = new User
            {
                NameFirst = "admin",
                NameLast = "admin",
                Phone = "37529144",
                Adress = "adress",
                Email = "admin@mail.ru",
                IsActive = true,
                UserName = "admin@mail.ru"
            };
            var result = await _userManager.CreateAsync(Supervisor, PASSWORD);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(Supervisor, SupervisorRole);
            }
        }

        protected virtual async Task SeedEmployeAsync()
        {
            var _userManager = (UserManager<User>)_server.Host.Services.GetService(typeof(UserManager<User>));
            var context = (ContextEF)_server.Host.Services.GetService(typeof(ContextEF));
            var departament = new Departament
            {
                Name = $"{nameof(Employe)}'s Department",
                Warehouse = new Warehouse
                {
                    Name = $"{nameof(Employe)}'s Department Warehouse",
                }
            };
            context.Set<Departament>().Add(departament);
            context.SaveChanges();
            Employe = new Employe
            {
                NameFirst = nameof(Employe),
                NameLast = nameof(Employe),
                Phone = "259132721",
                Adress = "adress",
                Email = $"{nameof(Employe)}@mail.ru",
                IsActive = true,
                UserName = $"{nameof(Employe)}@mail.ru",
                DepartamentId = departament.Id
            };
            await _userManager.CreateAsync(Employe, PASSWORD);
        }

        protected virtual async Task SeedManagerAsync()
        {
            var _userManager = (UserManager<User>)_server.Host.Services.GetService(typeof(UserManager<User>));
            var context = (ContextEF)_server.Host.Services.GetService(typeof(ContextEF));
            var departament = new Departament
            {
                Name = $"{nameof(Manager)}'s Department",
                Warehouse = new Warehouse
                {
                    Name = $"{nameof(Manager)}'s Department Warehouse",
                }
            };
            context.Set<Departament>().Add(departament);
            context.SaveChanges();
            Manager = new Manager
            {
                NameFirst = nameof(Manager),
                NameLast = nameof(Manager),
                Phone = "259132721",
                Adress = "adress",
                Email = $"{nameof(Manager)}@mail.ru",
                IsActive = true,
                UserName = $"{nameof(Manager)}@mail.ru",
                DepartamentId = departament.Id
            };
            await _userManager.CreateAsync(Manager, PASSWORD);
        }

        protected virtual async Task SeedClientAsync()
        {
            var _userManager = (UserManager<User>)_server.Host.Services.GetService(typeof(UserManager<User>));
            var context = (ContextEF)_server.Host.Services.GetService(typeof(ContextEF));
            context.SaveChanges();
            Client = new Client
            {
                NameFirst = nameof(Client),
                NameLast = nameof(Client),
                Phone = "259132721",
                Adress = "adress",
                Email = $"{nameof(Client)}@mail.ru",
                IsActive = true,
                UserName = $"{nameof(Client)}@mail.ru"
            };
            await _userManager.CreateAsync(Client, PASSWORD);
        }

        protected virtual void DeleteDb()
        {
            var context = (ContextEF)_server.Host.Services.GetService(typeof(ContextEF));
            context.Database.EnsureDeleted();
        }

        protected virtual string ConvertToJsonString<T>(T target)
        {
            return JsonConvert.SerializeObject(target, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }

        protected async Task SignInAsAsync(User user)
        {
            var values = new Dictionary<string, string>
            {
               { "userName", user.UserName },
               { "password", PASSWORD }
            };
            var content = new StringContent(JsonConvert.SerializeObject(values), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/auth/login", content);
            response.EnsureSuccessStatusCode();
            var responseJson = JsonConvert.DeserializeObject<JObject>(await response.Content.ReadAsStringAsync());
            if (!string.IsNullOrEmpty(responseJson["access_token"]?.ToString()))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {responseJson["access_token"].ToString()}");
            }
            else
            {
                throw new InvalidOperationException("Can't sign in : No access token");
            }
        }
    }
}
