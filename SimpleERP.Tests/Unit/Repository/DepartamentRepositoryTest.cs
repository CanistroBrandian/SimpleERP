using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities;
using SimpleERP.Data.Entities.WarehouseEntity;
using SimpleERP.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SimpleERP.Tests.Unit.Repository
{
    public class DepartamentRepositoryTest
    {

        private readonly DbContextOptions<ContextEF> _dbContextOptions;

        public DepartamentRepositoryTest()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            _dbContextOptions = new DbContextOptionsBuilder<ContextEF>().UseSqlite(connection).Options;
            using (var context = new ContextEF(_dbContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
            }
        }

        [Fact]
        public async Task GetAllDepartaments()
        {

            // arrange
            var departament = CreateDepartamentInDb();
            // act
            List<Departament> results = null;
            using (var context = new ContextEF(_dbContextOptions))
            {
                IDepartamentRepository repository = new DepartamentRepository(context);
                results = await repository.GetAllAsync();
            }
            var count = results.Count();

            // assert
            Assert.Equal(1, count);
       
        }


        [Fact]
        public async Task GetDepartamentById()
        {
            // arrange
            var departament = CreateDepartamentInDb();

            // act

            Departament result = null;
            using (var context = new ContextEF(_dbContextOptions))
            {
                IDepartamentRepository repository = new DepartamentRepository(context);
                result = await repository.GetSingleAsync(departament.Id);
            }

            // assert
            Assert.Equal(departament.Id, result.Id);
        }


      
        [Fact]
        public async Task CreateDepartament()
        {
            //arrange
            var warehouse = CreateWarehouseInDb();
            var departament = new Departament
            {
                Name = "Department1",
                WarehouseId = warehouse.Id
            };

            //act

            using (var context = new ContextEF(_dbContextOptions))
            {
                IDepartamentRepository repository = new DepartamentRepository(context);
                await repository.AddAsync(departament);
            }

            //assert
            using (var context = new ContextEF(_dbContextOptions))
            {
                var departamentDb = context.Departaments.FirstOrDefaultAsync(i => i.Id == departament.Id);
                Assert.NotNull(departament);
            }
        }


       

        [Fact]
        public async Task UpdateDepartament()
        {
            // arrange
            var departament = CreateDepartamentInDb();
            

            // act
            string newDepartamentName = departament.Name + " New";
            using (var context = new ContextEF(_dbContextOptions))
            {
                IDepartamentRepository repository = new DepartamentRepository(context);
                await repository.UpdateAsync(new Departament
                {
                    Id = departament.Id,
                    Name = newDepartamentName,
                    WarehouseId = departament.WarehouseId
                });

            }

            // assert

            using (var context = new ContextEF(_dbContextOptions))
            {
                var departamentFromDb = context.Set<Departament>().FirstOrDefault(s => s.Id == departament.Id);
                Assert.NotEqual(departament.Name, departamentFromDb.Name);
                Assert.Equal(newDepartamentName, departamentFromDb.Name);
            }
        }

        [Fact]
        public async Task DeleteDepartament()
        {
            // arranges
            var departament = CreateDepartamentInDb();

            // act

            using (var context = new ContextEF(_dbContextOptions))
            {
                IDepartamentRepository repository = new DepartamentRepository(context);
                await repository.DeleteAsync(departament.Id);
            }

            // assert

            using (var context = new ContextEF(_dbContextOptions))
            {
                var departamentFromDb = context.Set<Departament>().FirstOrDefault(s => s.Id == departament.Id);
                Assert.Null(departamentFromDb);
            }
        }

        protected void Dispose()
        {
            using (var context = new ContextEF(_dbContextOptions))
            {
                context.Database.EnsureDeleted();
            }
        }

        #region Helpers

            private Departament CreateDepartamentInDb()
        {
          
            using (var context = new ContextEF(_dbContextOptions))
            {

                var departament = new Departament
                {
                    Name = "Departament 1",
                    WarehouseId = CreateWarehouseInDb().Id
                };
                context.Set<Departament>().Add(departament);
                context.SaveChanges();
                return departament;
            }
        }

        private Warehouse CreateWarehouseInDb()
        {

            using (var context = new ContextEF(_dbContextOptions))
            {

                var warehouse = new Warehouse
                {
                    Name = "Warehouse 1",
                };

                context.Set<Warehouse>().Add(warehouse);
                context.SaveChanges();
                return warehouse;
            }
        }

        //private (Manager manager, Employe employe) CreateDepartamentDependenciesInDb()
        //{
        //    using (var context = new ContextEF(_dbContextOptions))
        //    {
        //        context.Set<Warehouse>().Add(new Warehouse
        //        {
        //            Id = 2,
        //            Name = "Warehouse2",
        //        });
        //        context.Set<Departament>().Add(new Departament
        //        {
        //            Id = 1,
        //            Name = "Department1",
        //            WarehouseId = 2

        //        });
        //        var manager = new Manager
        //        {
        //            Id = Guid.NewGuid().ToString(),
        //            Email = "mail@mail.ru",
        //            Adress = "adress",
        //            NameFirst = "name",
        //            NameLast = "namelast",
        //            DepartamentId = 1,
        //            PhoneNumber = "255555",
        //            UserName = "mail@mail.ru"
        //        };
        //        context.Set<Manager>().Add(manager);

        //        var employe = new Employe
        //        {
        //            Id = Guid.NewGuid().ToString(),
        //            Email = "mail@mail.ru",
        //            Adress = "adress",
        //            NameFirst = "name",
        //            NameLast = "namelast",
        //            DepartamentId = 1,
        //            PhoneNumber = "255555",
        //            UserName = "mail@mail.ru"
        //        };
        //        context.Set<Employe>().Add(employe);
        //        context.SaveChanges();

        //        return (manager, employe);
        //    }
        //}

        #endregion
    }
}
