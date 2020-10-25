using Microsoft.EntityFrameworkCore.Migrations;

namespace WebCore.Domain.Migrations
{
    public partial class titlemoduleentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Logs",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "AdministratorActions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Logs");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AdministratorActions");
        }
    }
}
