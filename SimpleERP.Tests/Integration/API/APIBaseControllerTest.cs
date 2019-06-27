using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SimpleERP.Data.Context;
using System;
using System.Net.Http;
using System.Reflection;

namespace SimpleERP.Tests.Integration.API
{
    public class APIBaseControllerTest : IDisposable
    {
        protected readonly TestServer _server;
        protected readonly HttpClient _client;

        public APIBaseControllerTest()
        {
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<TestStartup>()
                .UseSetting(WebHostDefaults.ApplicationKey, typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            _client = _server.CreateClient();
            CreateDb();
        }

        protected virtual void CreateDb()
        {
            var context = (ContextEF)_server.Host.Services.GetService(typeof(ContextEF));
            context.Database.EnsureCreated();
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

        public void Dispose()
        {
            DeleteDb();
        }
    }
}
