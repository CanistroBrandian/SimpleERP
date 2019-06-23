using Microsoft.EntityFrameworkCore.Migrations;

namespace SimpleERP.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeId",
                table: "Departmaentes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeId",
                table: "Departmaentes",
                nullable: false,
                defaultValue: 0);
        }
    }
}
