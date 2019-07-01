using SimpleERP.Abstract;
using SimpleERP.Controllers.API;
using SimpleERP.Data.Entities.OrderEntity;
using SimpleERP.Models.API.Order;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SimpleERP.Tests.Integration.API
{
    public class APIOrderControllerTest : APIBaseControllerTest
    {
        private readonly IOrderRepository _orderRepository;

        public APIOrderControllerTest() : base()
        {
            _orderRepository = (IOrderRepository)_server.Host.Services.GetService(typeof(IOrderRepository));
        }

        [Fact]
        public async Task GetAll_OK_Result_No_Items()
        {
            // Arrange
            await SeedSupervisor();

            await SignInAsAsync(Supervisor);

            // Act
            var response = await _httpClient.GetAsync($"{APIOrdersController.BASE_ROUTE}");
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
                await _orderRepository.AddAsync(new Order
                {
                    Information = $"Order {i + 1}",
                    Status=false

                });;
            }

            // Act
            var response = await _httpClient.GetAsync($"{APIOrdersController.BASE_ROUTE}");
            var expectedResult = ConvertToJsonString((await _orderRepository.GetAllAsync()).Select(f => new OrderModel
            {
                Information = f.Information,
                Status = f.Status,
                Id = f.Id
            }));

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            Assert.Equal(resultString, expectedResult);
        }

    }
    
}
