using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCore.Domain.Migrations
{
    public partial class orderindex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderIndex",
                table: "Logs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderIndex",
                table: "AdministratorActions",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderIndex",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "OrderIndex",
                table: "AdministratorActions");
        }
    }
}
