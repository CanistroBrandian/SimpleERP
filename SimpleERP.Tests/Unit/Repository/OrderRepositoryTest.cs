using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities.OrderEntity;
using SimpleERP.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SimpleERP.Tests.Unit.Repository
{
    public class OrderRepositoryTest
    {

        private readonly DbContextOptions<ContextEF> _dbContextOptions;

        public OrderRepositoryTest()
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
        public async Task GetAllOrders()
        {

            // arrange
            using (var context = new ContextEF(_dbContextOptions))
            {
                IOrderRepository repository = new OrderRepository(context);
                await repository.AddAsync(new Order
                {
                    Information = "Order test",
                    Status = false
                });
            }
            // act
            List<Order> results = null;
            using (var context = new ContextEF(_dbContextOptions))
            {
                IOrderRepository repository = new OrderRepository(context);
                results = await repository.GetAllAsync();
            }
            var count = results.Count();

            // assert
            Assert.Equal(1, count);
            Assert.Equal("Order test", results.First().Information);
        }


        [Fact]
        public async Task GetOrderById()
        {
            // arrange
            using (var context = new ContextEF(_dbContextOptions))
            {
                IOrderRepository repository = new OrderRepository(context);
                await repository.AddAsync(new Order
                {
                    Information = "Order test",
                    Status = false
                });
            }

            // act

            Order result = null;
            using (var context = new ContextEF(_dbContextOptions))
            {
                IOrderRepository repository = new OrderRepository(context);
                result = await repository.GetSingleAsync(1);
            }

            // assert
            Assert.Equal(1, result.Id);
        }


        [Fact]
        public async Task CreateOrder()
        {
            // arrange

            // act

            using (var context = new ContextEF(_dbContextOptions))
            {
                IOrderRepository repository = new OrderRepository(context);
                await repository.AddAsync(new Order
                {
                    Information = "Order test",
                    Status = false
                });
            }

            // assert

            Order result = null;

            using (var context = new ContextEF(_dbContextOptions))
            {
                result = context.Set<Order>().FirstOrDefault(s => s.Id == 1);
            }

            Assert.Equal(1, result.Id);
        }


        [Fact]
        public async Task UpdateOrder()
        {
            // arranges
            using (var context = new ContextEF(_dbContextOptions))
            {
                context.Set<Order>().Add(new Order
                {
                    Id = 1,
                    Information = "Order test",
                    Status = false
                });
                context.SaveChanges();
            }

            // act

            using (var context = new ContextEF(_dbContextOptions))
            {
                IOrderRepository repository = new OrderRepository(context);
                await repository.UpdateAsync(new Order
                {
                    Id = 1,
                    Information = "new name for order",
                    Status = false
                });
                context.SaveChanges();
            }

            // assert

            using (var context = new ContextEF(_dbContextOptions))
            {
                var orderFromDb = context.Set<Order>().FirstOrDefault(s => s.Id ==1);
                Assert.NotEqual("Order test", orderFromDb.Information);
                Assert.Equal("new name for order", orderFromDb.Information);
            }
        }

        [Fact]
        public async Task DeleteOrder()
        {
            // arranges
            using (var context = new ContextEF(_dbContextOptions))
            {
                context.Set<Order>().Add(new Order
                {
                    Information = "Test Order",
                    Status = false
                    
                });
                context.SaveChanges();
            }

            // act

            using (var context = new ContextEF(_dbContextOptions))
            {
                IOrderRepository repository = new OrderRepository(context);
                await repository.DeleteAsync(1);
            }

            // assert

            using (var context = new ContextEF(_dbContextOptions))
            {
                var orderFromDb = context.Set<Order>().FirstOrDefault(s => s.Id == 1);
                Assert.Null(orderFromDb);
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
