using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Models.Abstract;
using SimpleERP.Models.Context;
using SimpleERP.Models.Entities.WarehouseEntity;
using SimpleERP.Models.Repository;
using System;
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
            using (var context = new ContextEF(_dbContextOptions))
            {
                IWarehouseRepository repository = new WarehouseRepository(context);
                await repository.AddAsync(new Warehouse
                {
                    Name = "Test Warehouse"
                });
            }
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
            Assert.Equal("Test Warehouse", results.First().Name);
        }


        [Fact]
        public async Task GetWarehouseById()
        {
            // arrange
            using (var context = new ContextEF(_dbContextOptions))
            {
                IWarehouseRepository repository = new WarehouseRepository(context);
                await repository.AddAsync(new Warehouse
                {
                    Id = 1,
                    Name = "Test Warehouse"
                });
            }

            // act

            Warehouse result = null;
            using (var context = new ContextEF(_dbContextOptions))
            {
                IWarehouseRepository repository = new WarehouseRepository(context);
                result = await repository.GetSingleAsync(1);
            }

            // assert
            Assert.Equal(1, result.Id);
        }


        [Fact]
        public async Task CreateWarehouse()
        {
            // arrange

            // act

            using (var context = new ContextEF(_dbContextOptions))
            {
                IWarehouseRepository repository = new WarehouseRepository(context);
                await repository.AddAsync(new Warehouse
                {
                    Id = 1,
                    Name = "Test Warehouse"
                });
            }

            // assert

            Warehouse result = null;

            using (var context = new ContextEF(_dbContextOptions))
            {
                result = context.Set<Warehouse>().FirstOrDefault(s => s.Id == 1);
            }

            Assert.Equal(1, result.Id);
        }


        [Fact]
        public async Task UpdateWarehouse()
        {
            // arranges
            using (var context = new ContextEF(_dbContextOptions))
            {
                context.Set<Warehouse>().Add(new Warehouse
                {
                    Id = 1,
                    Name = "Test Warehouse"
                });
                context.SaveChanges();
            }

            // act

            using (var context = new ContextEF(_dbContextOptions))
            {
                IWarehouseRepository repository = new WarehouseRepository(context);
                await repository.UpdateAsync(new Warehouse
                {
                    Name = "New Name",
                    Id = 1
                });
            }

            // assert

            using (var context = new ContextEF(_dbContextOptions))
            {
                var warehouseFromDb = context.Set<Warehouse>().FirstOrDefault(s => s.Id == 1);
                Assert.NotEqual("Test Warehouse", warehouseFromDb.Name);
                Assert.Equal("New Name", warehouseFromDb.Name);
            }
        }

        [Fact]
        public async Task DeleteWarehouse()
        {
            // arranges
            using (var context = new ContextEF(_dbContextOptions))
            {
                context.Set<Warehouse>().Add(new Warehouse
                {
                    Id = 1,
                    Name = "Test Warehouse"
                });
                context.SaveChanges();
            }

            // act

            using (var context = new ContextEF(_dbContextOptions))
            {
                IWarehouseRepository repository = new WarehouseRepository(context);
                await repository.DeleteAsync(1);
            }

            // assert

            using (var context = new ContextEF(_dbContextOptions))
            {
                var warehouseFromDb = context.Set<Warehouse>().FirstOrDefault(s => s.Id == 1);
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
    }
}
