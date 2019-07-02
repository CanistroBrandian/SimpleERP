using SimpleERP.Abstract;
using SimpleERP.Controllers.API;
using SimpleERP.Data.Entities.WarehouseEntity;
using SimpleERP.Models.API.Product;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SimpleERP.Tests.Integration.API
{
    public class APIProductControllerTest : APIBaseControllerTest
    {
        private readonly IProductRepository _productRepository;

        public APIProductControllerTest() : base()
        {
            _productRepository = (IProductRepository)_server.Host.Services.GetService(typeof(IProductRepository));
        }

        [Fact]
        public async Task GetAll_OK_Result_No_Items()
        {
            // Arrange
            await SeedSupervisor();

            await SignInAsAsync(Supervisor);

            // Act
            var response = await _httpClient.GetAsync($"{APIProductsController.BASE_ROUTE}");
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
            for (int i = 0; i < 5; i++)
            {
                await _productRepository.AddAsync(new Product
                {
                    Name = $"Product {i + 1}"
                });
            }

            // Act
            var response = await _httpClient.GetAsync($"{APIProductsController.BASE_ROUTE}");
            var expectedResult = ConvertToJsonString((await _productRepository.GetAllAsync()).Select(f => new ProductModel
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
