//using Microsoft.EntityFrameworkCore;
//using Moq;
//using SimpleERP.Models.Abstract;
//using SimpleERP.Models.Concreate;
//using SimpleERP.Models.Context;
//using SimpleERP.Models.Entities.Auth;
//using Xunit;
//using System.Linq;

//namespace Test
//{

//    public class SampleDataTest
//    {

//        [Fact]
//        public void AddEmployeTestInDataBase()
//        {
//            // Arrange
//            var mockSet = new Mock<DbSet<User>>();
//            var mockContext = new Mock<ContextEF>();
//            mockContext.Setup(m => m.Users);
//            var repo = new UserRepository(mockContext.Object);
//            // Act
//            repo.AddUser(TestData());
//            // Assert
//            mockSet.Verify(m => m.Add(It.IsAny<User>()), Times.Once());
//            mockContext.Verify(m => m.SaveChanges(), Times.Once());
//        }
//        private User TestData()
//        {
//            return new User
//            {
//                NameFirst = "Bob",
//                NameLast = "Niohert",
//                Phone = "375214885",
//                Adress = "ul.malinia",
//                Login = "Login",
//                Password = "Pass"
//            };
//        }


//        [Fact]
//        public void TestAddUserInMemory()
//        {
//            IUserRepository repo = GetInMemoryUserRepository();
//            User user = new User()
//            {
//                NameFirst = "Bob",
//                NameLast = "Niohert",
//                Phone = "375214885",
//                Adress = "ul.malinia",
//                Login = "Login",
//                Password = "Pass"
//            };
//            User savedUser = repo.AddUser(user);
//            Assert.Equal("1", repo.GetUsers().Count.ToString());
//            Assert.Equal("Bob", savedUser.NameFirst);

//        }

//        private IUserRepository GetInMemoryUserRepository()
//        {
//            DbContextOptions<ContextEF> options;
//            var builder = new DbContextOptionsBuilder<ContextEF>();
//            builder.UseInMemoryDatabase();
//            options = builder.Options;
//            ContextEF context = new ContextEF(options);
//            context.Database.EnsureDeleted();
//            context.Database.EnsureDeleted();
//            return new UserRepository(context);
//        }

//        [Fact]
//        public void TestAddEmployeInMemory()
//        {
//            IEmployeRepository repo = GetInMemoryEmployeRepository();
//            Employe employe = new Employe()
//            {
//                NameFirst = "Bob",
//                NameLast = "Niohert",
//                Phone = "375214885",
//                Adress = "ul.malinia",
//                Login = "Login",
//                Password = "Pass",
//                DepartamentId = 1
//            };
//            Employe savedUser = repo.AddEmployee(employe);
//            Assert.Equal("1", repo.GetEmployes().Count.ToString());
//            Assert.Equal("Bob", savedUser.NameFirst);
//            // Assert.Equal("Employe", savedUser.Discriminator);
//        }

//        private IEmployeRepository GetInMemoryEmployeRepository()
//        {
//            DbContextOptions<ContextEF> options;
//            var builder = new DbContextOptionsBuilder<ContextEF>();
//            builder.UseInMemoryDatabase();
//            options = builder.Options;
//            ContextEF context = new ContextEF(options);
//            context.Database.EnsureDeleted();
//            context.Database.EnsureDeleted();
//            return new EmployeRepository(context);
//        }

//        [Fact]
//        public void TestAddManagerInMemory()
//        {
//            var options = new DbContextOptionsBuilder<ContextEF>()
//                .UseInMemoryDatabase(databaseName: "DbTest")
//                .Options;

//            // Run the test against one instance of the context
//            using (var context = new ContextEF(options))
//            {
//                var repo = new ManagerRepository(context);
//               var man = repo.AddManager(AddDataTest());
            
//            // Use a separate instance of the context to verify correct data was saved to database
            
//              //  Assert.Equal("1", repo.GetManagers().Count.ToString());
//               // Assert.Equal("Bob", context.Managers.Find("Bob").NameFirst);
//              //  Assert.Equal("Manager", context.Managers.Where(m => m. ));
//            }
//        }
//        private Manager AddDataTest()
//        {
//            return new Manager()
//            {
//                NameFirst = "Bob",
//                NameLast = "Niohert",
//                Phone = "375214885",
//                Adress = "ul.malinia",
//                Login = "Login",
//                Password = "Pass",
//                DepartamentId = 1
//            };
//        }
       
//    }
//}


