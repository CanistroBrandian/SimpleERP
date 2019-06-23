using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleERP.Migrations
{
    public partial class modelfixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeClients_Orders_OrderId",
                table: "EmployeClients");

            migrationBuilder.DropIndex(
                name: "IX_EmployeClients_OrderId",
                table: "EmployeClients");

            migrationBuilder.DropColumn(
                name: "DepartamentId",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "EmployeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "EmployeClients");

            migrationBuilder.DropColumn(
                name: "Login",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Password",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartamentId",
                table: "Warehouses",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ClientId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmployeId",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Orders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "EmployeClients",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Login",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Password",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeClients_OrderId",
                table: "EmployeClients",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeClients_Orders_OrderId",
                table: "EmployeClients",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
