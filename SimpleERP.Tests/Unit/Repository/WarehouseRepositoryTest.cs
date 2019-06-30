using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.WarehouseEntity;
using SimpleERP.Data.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SimpleERP.Tests.Unit.Repository
{
    public class WarehouseRepositoryTest
    {

        private readonly DbContextOptions<ContextEF> _dbContextOptions;

        public WarehouseRepositoryTest()
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
        public async Task GetAllWarehouses()
        {
            // arrange
            var warehouse = CreateWarehouseInDB();
            // act
            List<Warehouse> results = null;
            using (var context = new ContextEF(_dbContextOptions))
            {
                IWarehouseRepository repository = new WarehouseRepository(context);
                results = await repository.GetAllAsync();
            }
            var count = results.Count();

            // assert
            Assert.Equal(1, count);
        }


        [Fact]
        public async Task GetWarehouseById()
        {
            // arrange
            var warehouse = CreateWarehouseInDB();

            // act
            Warehouse result = null;
            using (var context = new ContextEF(_dbContextOptions))
            {
                IWarehouseRepository repository = new WarehouseRepository(context);
                result = await repository.GetSingleAsync(warehouse.Id);
            }

            // assert
            Assert.Equal(warehouse.Id, result.Id);
        }


        [Fact]
        public async Task CreateWarehouse()
        {
            // arrange
            var warehouse = new Warehouse
            {
                Name = "Test Warehouse"
            };
            // act

            using (var context = new ContextEF(_dbContextOptions))
            {
                IWarehouseRepository repository = new WarehouseRepository(context);
                await repository.AddAsync(warehouse);
            }

            // assert

            Warehouse result = null;

            using (var context = new ContextEF(_dbContextOptions))
            {
                result = context.Set<Warehouse>().FirstOrDefault(s => s.Id == warehouse.Id);
            }

            Assert.Equal(warehouse.Id, result.Id);
        }


        [Fact]
        public async Task UpdateWarehouse()
        {
            // arranges

            var warehouse = CreateWarehouseInDB();
            string newName = warehouse.Name + " New Name";

            // act

            using (var context = new ContextEF(_dbContextOptions))
            {
                IWarehouseRepository repository = new WarehouseRepository(context);
                await repository.UpdateAsync(new Warehouse
                {
                    Id = warehouse.Id,
                    Name = newName,
                });
            }

            // assert

            using (var context = new ContextEF(_dbContextOptions))
            {
                var warehouseFromDb = context.Set<Warehouse>().FirstOrDefault(s => s.Id == warehouse.Id);
                Assert.NotEqual(warehouse.Name, warehouseFromDb.Name);
                Assert.Equal(newName, warehouseFromDb.Name);
            }
        }

        [Fact]
        public async Task DeleteWarehouse()
        {
            // arranges
            var warehouse = CreateWarehouseInDB();

            // act

            using (var context = new ContextEF(_dbContextOptions))
            {
                IWarehouseRepository repository = new WarehouseRepository(context);
                await repository.DeleteAsync(warehouse.Id);
            }

            // assert

            using (var context = new ContextEF(_dbContextOptions))
            {
                var warehouseFromDb = context.Set<Warehouse>().FirstOrDefault(s => s.Id == warehouse.Id);
                Assert.Null(warehouseFromDb);
            }
        }

        protected void Dispose()
        {
            using (var context = new ContextEF(_dbContextOptions))
            {
                context.Database.EnsureDeleted();
            }
        }
        private Warehouse CreateWarehouseInDB()
        {
            using (var context = new ContextEF(_dbContextOptions))
            {
                var warehouse = new Warehouse
                {
                    Name = "Warehouse1"
                };
                context.Set<Warehouse>().Add(warehouse);
                context.SaveChanges();
                return warehouse;
            }
        }
    }
}
