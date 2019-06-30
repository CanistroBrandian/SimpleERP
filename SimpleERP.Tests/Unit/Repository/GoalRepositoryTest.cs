using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Abstract;
using SimpleERP.Data.Context;
using SimpleERP.Data.Entities;
using SimpleERP.Data.Entities.Auth;
using SimpleERP.Data.Entities.GoalEntity;
using SimpleERP.Data.Entities.WarehouseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SimpleERP.Tests.Unit.Repository
{
    public class GoalRepositoryTest
    {
        private readonly DbContextOptions<ContextEF> _dbContextOptions;

        public GoalRepositoryTest()
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
        public async Task GetAllGoals()
        {

            // arrange
            var goal = CreateGoalInDb();
            // act
            List<Goal> results = null;
            using (var context = new ContextEF(_dbContextOptions))
            {
                IGoalRepository repository = new GoalRepository(context);
                results = await repository.GetAllAsync();
            }
            var count = results.Count();

            // assert
            Assert.Equal(1, count);
            Assert.Equal("Name Goal", results.First().Name);
        }


        [Fact]
        public async Task GetGoalById()
        {
            // arrange
            var goal = CreateGoalInDb();

            // act

            Goal result = null;
            using (var context = new ContextEF(_dbContextOptions))
            {
                IGoalRepository repository = new GoalRepository(context);
                result = await repository.GetSingleAsync(goal.Id);
            }

            // assert
            Assert.Equal(goal.Id, result.Id);
        }


        [Fact]
        public async Task CreateGoal_AssigneOrReporterDoesntExist_ThrowException()
        {

            using (var context = new ContextEF(_dbContextOptions))
            {
                IGoalRepository repository = new GoalRepository(context);
                await Assert.ThrowsAsync<DbUpdateException>(() => repository.AddAsync(new Goal
                {
                    Id = 1,
                    Name = "Name Goal",
                    Description = "Description Goal",
                    AssigneId = Guid.NewGuid().ToString(),
                    ReporterId = Guid.NewGuid().ToString(),
                    DateCreated = new DateTime(2018, 02, 15),
                    DateFinished = new DateTime(2018, 02, 16),
                }));
            }
        }
        [Fact]
        public async Task CreateGoal()
        {
            //arrange
            var goalDependencies = CreateGoalDependenciesInDb();

            //act
            var newGoal = new Goal
            {
                Id = 1,
                Name = "Name Goal",
                Description = "Description Goal",
                AssigneId = goalDependencies.employe.Id,
                ReporterId = goalDependencies.manager.Id,
                DateCreated = new DateTime(2018, 02, 15),
                DateFinished = new DateTime(2018, 02, 16),
            };
            using (var context = new ContextEF(_dbContextOptions))
            {
                IGoalRepository repository = new GoalRepository(context);
                await repository.AddAsync(newGoal);
            }

            //assert
            using (var context = new ContextEF(_dbContextOptions))
            {
                var goal = context.Goals.FirstOrDefaultAsync(i => i.Id == newGoal.Id);
                Assert.NotNull(goal);
            }
        }


        [Fact]
        public async Task UpdateGoal_AssigneOrReporterDoesntExist_ThrowException()
        {
            // arrange
            var goal = CreateGoalInDb();

            // act
            string newGoalName = goal.Name + " New";
            using (var context = new ContextEF(_dbContextOptions))
            {
                IGoalRepository repository = new GoalRepository(context);

                // assert
                await Assert.ThrowsAsync<DbUpdateException>(() => repository.UpdateAsync(new Goal
                {
                    Id = goal.Id,
                    Name = newGoalName,
                    Description = goal.Description,
                    AssigneId = goal.AssigneId + " New",
                    ReporterId = goal.ReporterId + " New",
                    DateCreated = goal.DateCreated,
                    DateFinished = goal.DateFinished,
                }));

            }
        }

        [Fact]
        public async Task UpdateGoal()
        {
            // arrange
            var goal = CreateGoalInDb();

            // act
            string newGoalName = goal.Name + " New";
            using (var context = new ContextEF(_dbContextOptions))
            {
                IGoalRepository repository = new GoalRepository(context);
                await repository.UpdateAsync(new Goal
                {
                    Id = goal.Id,
                    Name = newGoalName,
                    Description = goal.Description,
                    AssigneId = goal.AssigneId,
                    ReporterId = goal.ReporterId,
                    DateCreated = goal.DateCreated,
                    DateFinished = goal.DateFinished,
                });

            }

            // assert

            using (var context = new ContextEF(_dbContextOptions))
            {
                var goalFromDb = context.Set<Goal>().FirstOrDefault(s => s.Id == goal.Id);
                Assert.NotEqual(goal.Name, goalFromDb.Name);
                Assert.Equal(newGoalName, goalFromDb.Name);
            }
        }

        [Fact]
        public async Task DeleteGoal()
        {
            // arranges
            var goal = CreateGoalInDb();

            // act

            using (var context = new ContextEF(_dbContextOptions))
            {
                IGoalRepository repository = new GoalRepository(context);
                await repository.DeleteAsync(goal.Id);
            }

            // assert

            using (var context = new ContextEF(_dbContextOptions))
            {
                var goalFromDb = context.Set<Goal>().FirstOrDefault(s => s.Id == goal.Id);
                Assert.Null(goalFromDb);
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

        private Goal CreateGoalInDb()
        {
            var goalDependencies = CreateGoalDependenciesInDb();
            using (var context = new ContextEF(_dbContextOptions))
            {
                var goal = new Goal
                {
                    Name = "Name Goal",
                    Description = "Description Goal",
                    AssigneId = goalDependencies.employe.Id,
                    ReporterId = goalDependencies.manager.Id,
                    DateCreated = new DateTime(2018, 02, 15),
                    DateFinished = new DateTime(2018, 02, 16),

                };
                context.Set<Goal>().Add(goal);
                context.SaveChanges();
                return goal;
            }
        }

        private (Manager manager, Employe employe) CreateGoalDependenciesInDb()
        {
            using (var context = new ContextEF(_dbContextOptions))
            {
                context.Set<Warehouse>().Add(new Warehouse
                {
                    Id = 2,
                    Name = "Warehouse2",
                });
                context.Set<Departament>().Add(new Departament
                {
                    Id = 1,
                    Name = "Department1",
                    WarehouseId = 2

                });
                var manager = new Manager
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "mail@mail.ru",
                    Adress = "adress",
                    NameFirst = "name",
                    NameLast = "namelast",
                    DepartamentId = 1,
                    PhoneNumber = "255555",
                    UserName = "mail@mail.ru"
                };
                context.Set<Manager>().Add(manager);

                var employe = new Employe
                {
                    Id = Guid.NewGuid().ToString(),
                    Email = "mail@mail.ru",
                    Adress = "adress",
                    NameFirst = "name",
                    NameLast = "namelast",
                    DepartamentId = 1,
                    PhoneNumber = "255555",
                    UserName = "mail@mail.ru"
                };
                context.Set<Employe>().Add(employe);
                context.SaveChanges();

                return (manager, employe);
            }
        }

        #endregion
    }
}