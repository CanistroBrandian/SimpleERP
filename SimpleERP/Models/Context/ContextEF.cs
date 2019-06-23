using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SimpleERP.Models.Entities;
using SimpleERP.Models.Entities.Auth;
using SimpleERP.Models.Entities.GoalEntity;
using SimpleERP.Models.Entities.OrderEntity;
using SimpleERP.Models.Entities.WarehouseEntity;

namespace SimpleERP.Models.Context
{
    public class ContextEF : IdentityDbContext<User>
    {
        public ContextEF(DbContextOptions<ContextEF> options) : base(options)
        {
           // Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ClientOrder>().HasKey(sc => new { sc.ClientId, sc.OrderId });
            modelBuilder.Entity<EmployeClient>().HasKey(ec => new { ec.ClientId, ec.EmployeId });
            modelBuilder.Entity<EmployeOrder>().HasKey(eo => new { eo.OrderId, eo.EmployeId });
            modelBuilder.Entity<OrderProduct>().HasKey(op => new { op.OrderId, op.ProductId });
            modelBuilder.Entity<Stock>().HasKey(s => new { s.WarehouseId, s.ProductId });

            ////связь по внешним ключам Таблицы Clint и Order
            //modelBuilder.Entity<ClientOrder>().
            //    HasOne<Client>(co => co.Client).
            //    WithMany(c => c.ClientOrders).
            //    HasForeignKey(co => co.ClientId);
            //modelBuilder.Entity<ClientOrder>().
            //    HasOne<Order>(co => co.Order).
            //    WithMany(o => o.ClientOrders).
            //    HasForeignKey(c => c.OrderId);

            // связь по внешним ключам Employe и User
            modelBuilder.Entity<EmployeClient>().
               HasOne<Employe>(em => em.Employe).
               WithMany(c => c.EmployeClients).
               HasForeignKey(em => em.EmployeId).OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmployeClient>().
                HasOne<Client>(o => o.Client).
                WithMany(em => em.EmployeClients).
                HasForeignKey(c => c.ClientId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Goal>()
                .HasOne(f => f.Reporter)
                .WithMany(f => f.CreatedGoals)
                .HasForeignKey(f => f.ReporterId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Goal>()
                .HasOne(f => f.Assigne)
                .WithMany(f => f.Goals)
                .HasForeignKey(f => f.AssigneId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Manager>()
                .HasMany(f => f.CreatedGoals)
                .WithOne(f => f.Reporter)
                .IsRequired()
                .HasForeignKey(f => f.ReporterId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Employe>()
                .HasMany(f => f.Goals)
                .WithOne(f => f.Assigne)
                .IsRequired()
                .HasForeignKey(f => f.AssigneId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>();
        }

      

        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientOrder> ClientOrders { get; set; }
        public DbSet<Employe> Employees { get; set; }
        public DbSet<EmployeClient> EmployeClients { get; set; }
        public DbSet<EmployeOrder> EmployeOrders { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Departament> Departaments { get; set; }
        public DbSet<Goal> Goals { get; set; }
    }
}
