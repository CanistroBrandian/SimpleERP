using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleERP.Migrations
{
    public partial class add_field_IsActive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AspNetUsers");
        }
    }
}
