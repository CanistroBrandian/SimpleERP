﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleERP.Data.Context;

namespace SimpleERP.Migrations
{
    [DbContext(typeof(ContextEF))]
    [Migration("20190626185548_add_field_IsActive")]
    partial class add_field_IsActive
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.8-servicing-32085")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.Auth.ClientOrder", b =>
                {
                    b.Property<string>("ClientId");

                    b.Property<int>("OrderId");

                    b.Property<int>("Id");

                    b.HasKey("ClientId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("ClientOrders");
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.Auth.EmployeClient", b =>
                {
                    b.Property<string>("ClientId");

                    b.Property<string>("EmployeId");

                    b.Property<int>("Id");

                    b.HasKey("ClientId", "EmployeId");

                    b.HasIndex("EmployeId");

                    b.ToTable("EmployeClients");
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.Auth.EmployeOrder", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<string>("EmployeId");

                    b.Property<int>("Id");

                    b.HasKey("OrderId", "EmployeId");

                    b.HasIndex("EmployeId");

                    b.ToTable("EmployeOrders");
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.Auth.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Adress");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("IsActive");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NameFirst");

                    b.Property<string>("NameLast");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Phone");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.Departament", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.Property<int>("WarehouseId");

                    b.HasKey("Id");

                    b.HasIndex("WarehouseId");

                    b.ToTable("Departaments");
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.GoalEntity.Goal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssigneId")
                        .IsRequired();

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime>("DateFinished");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<string>("ReporterId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("AssigneId");

                    b.HasIndex("ReporterId");

                    b.ToTable("Goals");
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.OrderEntity.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Information");

                    b.Property<bool>("Status");

                    b.HasKey("Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.OrderEntity.OrderProduct", b =>
                {
                    b.Property<int>("OrderId");

                    b.Property<int>("ProductId");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.WarehouseEntity.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.WarehouseEntity.Stock", b =>
                {
                    b.Property<int>("WarehouseId");

                    b.Property<int>("ProductId");

                    b.Property<int>("Count");

                    b.HasKey("WarehouseId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("Stocks");
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.WarehouseEntity.Warehouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Warehouses");
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.Auth.Client", b =>
                {
                    b.HasBaseType("SimpleERP.Data.Entities.Auth.User");


                    b.ToTable("Client");

                    b.HasDiscriminator().HasValue("Client");
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.Auth.Employe", b =>
                {
                    b.HasBaseType("SimpleERP.Data.Entities.Auth.User");

                    b.Property<int>("DepartamentId");

                    b.HasIndex("DepartamentId");

                    b.ToTable("Employe");

                    b.HasDiscriminator().HasValue("Employe");
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.Auth.Manager", b =>
                {
                    b.HasBaseType("SimpleERP.Data.Entities.Auth.Employe");


                    b.ToTable("Manager");

                    b.HasDiscriminator().HasValue("Manager");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SimpleERP.Data.Entities.Auth.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SimpleERP.Data.Entities.Auth.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimpleERP.Data.Entities.Auth.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SimpleERP.Data.Entities.Auth.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.Auth.ClientOrder", b =>
                {
                    b.HasOne("SimpleERP.Data.Entities.Auth.Client", "Client")
                        .WithMany("ClientOrders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimpleERP.Data.Entities.OrderEntity.Order", "Order")
                        .WithMany("ClientOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.Auth.EmployeClient", b =>
                {
                    b.HasOne("SimpleERP.Data.Entities.Auth.Client", "Client")
                        .WithMany("EmployeClients")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SimpleERP.Data.Entities.Auth.Employe", "Employe")
                        .WithMany("EmployeClients")
                        .HasForeignKey("EmployeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.Auth.EmployeOrder", b =>
                {
                    b.HasOne("SimpleERP.Data.Entities.Auth.Employe", "Employe")
                        .WithMany()
                        .HasForeignKey("EmployeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimpleERP.Data.Entities.OrderEntity.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.Departament", b =>
                {
                    b.HasOne("SimpleERP.Data.Entities.WarehouseEntity.Warehouse", "Warehouse")
                        .WithMany("Departaments")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.GoalEntity.Goal", b =>
                {
                    b.HasOne("SimpleERP.Data.Entities.Auth.Employe", "Assigne")
                        .WithMany("Goals")
                        .HasForeignKey("AssigneId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SimpleERP.Data.Entities.Auth.Manager", "Reporter")
                        .WithMany("CreatedGoals")
                        .HasForeignKey("ReporterId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.OrderEntity.OrderProduct", b =>
                {
                    b.HasOne("SimpleERP.Data.Entities.OrderEntity.Order", "Order")
                        .WithMany("OrderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimpleERP.Data.Entities.WarehouseEntity.Product", "Product")
                        .WithMany("OrderProducts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.WarehouseEntity.Stock", b =>
                {
                    b.HasOne("SimpleERP.Data.Entities.WarehouseEntity.Product", "Product")
                        .WithMany("Stocks")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SimpleERP.Data.Entities.WarehouseEntity.Warehouse", "Warehouse")
                        .WithMany("Products")
                        .HasForeignKey("WarehouseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SimpleERP.Data.Entities.Auth.Employe", b =>
                {
                    b.HasOne("SimpleERP.Data.Entities.Departament", "Departament")
                        .WithMany("Employees")
                        .HasForeignKey("DepartamentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
