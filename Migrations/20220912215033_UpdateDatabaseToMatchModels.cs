using Microsoft.EntityFrameworkCore.Migrations;

namespace ControlRisksAcademy.Migrations
{
    public partial class UpdateDatabaseToMatchModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Teachers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Students",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Active",
                table: "Teachers");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Students");
        }
    }
}
