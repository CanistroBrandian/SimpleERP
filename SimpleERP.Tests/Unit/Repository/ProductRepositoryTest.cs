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
    public class ProductRepositoryTest
    {
        private readonly DbContextOptions<ContextEF> _dbContextOptions;

        public ProductRepositoryTest()
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
        public async Task GetAllProducts()
        {

            // arrange
            using (var context = new ContextEF(_dbContextOptions))
            {
                IProductRepository repository = new ProductRepository(context);
                await repository.AddAsync(new Product
                {
                   Id = 1,
                   Name = "Name product",
                   Description = "Description product"                   
                });
            }
            // act
            List<Product> results = null;
            using (var context = new ContextEF(_dbContextOptions))
            {
                IProductRepository repository = new ProductRepository(context);
                results = await repository.GetAllAsync();
            }
            var count = results.Count();

            // assert
            Assert.Equal(1, count);
            Assert.Equal("Name product", results.First().Name);
        }


        [Fact]
        public async Task GetProductById()
        {
            // arrange
            using (var context = new ContextEF(_dbContextOptions))
            {
                IProductRepository repository = new ProductRepository(context);
                await repository.AddAsync(new Product
                {
                    Id = 1,
                    Name = "Name product",
                    Description = "Description product"
                });
            }

            // act

            Product result = null;
            using (var context = new ContextEF(_dbContextOptions))
            {
                IProductRepository repository = new ProductRepository(context);
                result = await repository.GetSingleAsync(1);
            }

            // assert
            Assert.Equal(1, result.Id);
        }


        [Fact]
        public async Task CreateProduct()
        {
            // arrange

            // act

            using (var context = new ContextEF(_dbContextOptions))
            {
                IProductRepository repository = new ProductRepository(context);
                await repository.AddAsync(new Product
                {
                    Id = 1,
                    Name = "Name product",
                    Description = "Description product"
                });
            }

            // assert

            Product result = null;

            using (var context = new ContextEF(_dbContextOptions))
            {
                result = context.Set<Product>().FirstOrDefault(s => s.Id == 1);
            }

            Assert.Equal(1, result.Id);
        }


        [Fact]
        public async Task UpdateProduct()
        {
            // arranges
            using (var context = new ContextEF(_dbContextOptions))
            {
                context.Set<Product>().Add(new Product
                {
                    Id = 1,
                    Name = "Name product",
                    Description = "Description product"
                });
                context.SaveChanges();
            }

            // act

            using (var context = new ContextEF(_dbContextOptions))
            {
                IProductRepository repository = new ProductRepository(context);
                await repository.UpdateAsync(new Product
                {
                    Id = 1,
                    Name = "new name for product",
                    Description = "Description product"
                });
                context.SaveChanges();
            }

            // assert

            using (var context = new ContextEF(_dbContextOptions))
            {
                var ProductFromDb = context.Set<Product>().FirstOrDefault(s => s.Id == 1);
                Assert.NotEqual("Name product", ProductFromDb.Name);
                Assert.Equal("new name for product", ProductFromDb.Name);
            }
        }

        [Fact]
        public async Task DeleteProduct()
        {
            // arranges
            using (var context = new ContextEF(_dbContextOptions))
            {
                context.Set<Product>().Add(new Product
                {
                    Id = 1,
                    Name = "Name product",
                    Description = "Description product"

                });
                context.SaveChanges();
            }

            // act

            using (var context = new ContextEF(_dbContextOptions))
            {
                IProductRepository repository = new ProductRepository(context);
                await repository.DeleteAsync(1);
            }

            // assert

            using (var context = new ContextEF(_dbContextOptions))
            {
                var ProductFromDb = context.Set<Product>().FirstOrDefault(s => s.Id == 1);
                Assert.Null(ProductFromDb);
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
