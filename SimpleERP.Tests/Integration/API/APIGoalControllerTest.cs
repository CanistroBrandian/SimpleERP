using SimpleERP.Abstract;
using SimpleERP.Controllers.API;
using SimpleERP.Data.Entities.GoalEntity;
using SimpleERP.Models.API.Goal;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SimpleERP.Tests.Integration.API
{
    public class APIGoalControllerTest : APIBaseControllerTest
    {
        private readonly IGoalRepository _goalRepository;


        public APIGoalControllerTest() : base()
        {
            _goalRepository = (IGoalRepository)_server.Host.Services.GetService(typeof(IGoalRepository));
        }

        [Fact]
        public async Task GetAll_OK_Result_No_Items()
        {
            // Arrange
            await SeedManagerAsync();
            await SignInAsAsync(Manager);

            // Act
            var response = await _httpClient.GetAsync($"{APIGoalsController.BASE_ROUTE}");
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

            await SeedManagerAsync();
            await SeedEmployeAsync();
            await SignInAsAsync(Manager);
            for (int i = 0; i < 5; i++)
            {
               
                await _goalRepository.AddAsync(new Goal
                {
                    Name = $"Goal {i + 1}",
                    Description = $"Goal {i + 1}",
                    AssigneId = Manager.Id,
                    ReporterId = Employe.Id,
                    DateCreated = new DateTime(),
                    DateFinished = new DateTime(),
                }); 
            }

            // Act
            var response = await _httpClient.GetAsync($"{APIGoalsController.BASE_ROUTE}");
            var expectedResult = ConvertToJsonString((await _goalRepository.GetAllAsync()).Select(f => new GoalModel
            {
               
                Name = f.Name,
                Description = f.Description,
                AssigneId = f.AssigneId,
                ReporterId = f.ReporterId,
                DateCreated = f.DateCreated,
                DateFinished = f.DateFinished,
            }));

            // Assert
            response.EnsureSuccessStatusCode();
            var resultString = await response.Content.ReadAsStringAsync();
            Assert.Equal(resultString, expectedResult);
        }

    }
}
