using SimpleERP.Models.Abstract;
using SimpleERP.Models.API.Warehouse;
using SimpleERP.Models.Entities.WarehouseEntity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SimpleERP.Tests.API
{
    public class APIWarehouseControllerTest : APIBaseControllerTest
    {
        private readonly IWarehouseRepository _warehouseRepository;

        private const string BASE_ROUTE = "api/warehouse";

        public APIWarehouseControllerTest() : base()
        {
            _warehouseRepository = (IWarehouseRepository)_server.Host.Services.GetService(typeof(IWarehouseRepository));

        }

        [Fact]
        public async Task GetAll_OK_Result_No_Items()
        {
            // Act
            var response = await _client.GetAsync($"{BASE_ROUTE}");
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
            for (int i = 0; i < 5; i++)
            {
                await _warehouseRepository.AddAsync(new Warehouse
                {
                    Name = $"Warehouse {i + 1}"
                });
            }

            // Act
            var response = await _client.GetAsync($"{BASE_ROUTE}");
            var expectedResult = ConvertToJsonString((await _warehouseRepository.GetAllAsync()).Select(f => new WarehouseModel
            {
                Name = f.Name,
                Id = f.Id
            }));

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            Assert.Equal(resultString, expectedResult);
        }

    }
}
