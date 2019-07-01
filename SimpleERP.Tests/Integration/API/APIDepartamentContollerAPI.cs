using SimpleERP.Abstract;
using SimpleERP.Controllers.API;
using SimpleERP.Data.Entities;
using SimpleERP.Data.Entities.WarehouseEntity;
using SimpleERP.Models.API.Departament;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SimpleERP.Tests.Integration.API
{
    public class APIDepartamentContollerTest : APIBaseControllerTest
    {
        private readonly IDepartamentRepository _departamentRepository;
        private readonly IWarehouseRepository _warehouseRepository;


        public APIDepartamentContollerTest() : base()
        {
            _departamentRepository = (IDepartamentRepository)_server.Host.Services.GetService(typeof(IDepartamentRepository));
            _warehouseRepository = (IWarehouseRepository)_server.Host.Services.GetService(typeof(IWarehouseRepository));
        }

        [Fact]
        public async Task GetAll_OK_Result_No_Items()
        {
            // Arrange
            await SeedSupervisor();

            await SignInAsAsync(Supervisor);

            // Act
            var response = await _httpClient.GetAsync($"{APIDepartamentsController.BASE_ROUTE}");
            var expectedResult = ConvertToJsonString(Array.Empty<object>());

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            Assert.Equal(resultString, expectedResult);
        }

        [Fact]
        public async Task GetAll_OK_Result_Has_Items()
        {
            // Arrange
            await SeedSupervisor();
            await SignInAsAsync(Supervisor);
            for (int i = 1; i <= 5; i++)
            {
                var warehouse = new Warehouse
                {
                    Id = i,
                    Name = $"Warehouse {i}",
                };
                var departament = new Departament
                {
                    Name = $"Departament {i}",
                    WarehouseId = warehouse.Id
                };
                await _warehouseRepository.AddAsync(warehouse);
                await _departamentRepository.AddAsync(departament);

            }

            // Act
            var response = await _httpClient.GetAsync($"{APIDepartamentsController.BASE_ROUTE}");
            var expectedResult = ConvertToJsonString((await _departamentRepository.GetAllAsync()).Select(f => new DepartamentModel
            {
                Name = f.Name,
                Id = f.Id,
                WarehouseId = f.WarehouseId
            }));

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            Assert.Equal(resultString, expectedResult);
        }

    }

}
