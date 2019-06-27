using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleERP.Migrations
{
    public partial class add_field_IsActive2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "EmployeOrders");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "EmployeClients");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ClientOrders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EmployeOrders",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "EmployeClients",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ClientOrders",
                nullable: false,
                defaultValue: 0);
        }
    }
}
