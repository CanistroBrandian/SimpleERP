using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleERP.Controllers;
using SimpleERP.Models.Abstract;
using SimpleERP.Models.Entities.Auth;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Test
{
    public class SampleDataTest
    {
        [Fact]
        public void ReturnListEmploye()
        {
            // Arrange
            var mock = new Mock<IEmployeRepository>();
            mock.Setup(repo => repo.GetEmployes()).Returns(GetTestEmploye());
            var controller = new HomeController(mock.Object);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<List<Employe>>(
                viewResult.Model);
            Assert.Equal(GetTestEmploye().Count, model.Count());
        }
        private List<Employe> GetTestEmploye()
        {
            var employes = new List<Employe>
            {
                new Employe { 
                     NameFirst="Bob",
                    NameLast ="Niohert",
                    Phone ="375214885",
                    Adress ="ul.malinia",
                },
                new Employe { 
                     NameFirst="Cuasana",
                    NameLast ="Koloreta",
                    Phone ="14889223",
                    Adress ="ul.malinia"
                },
                new Employe {
                     NameFirst="reko",
                    NameLast ="123",
                    Phone ="123",
                    Adress ="ul.verto"
                },
                new Employe { Id=6,
                    NameFirst="qqqqqq",
                    NameLast ="Niohert",
                    Phone ="375214885",
                    Adress ="ul.malinia"
                },
            };
            return employes;
        }
    }
}
