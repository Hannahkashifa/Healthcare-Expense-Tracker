using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalHealthcareExpense.API.Migrations
{
    public partial class TestMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dosage",
                table: "Medicines");

            migrationBuilder.AddColumn<int>(
                name: "AfternoonDose",
                table: "Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MorningDose",
                table: "Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NightDose",
                table: "Medicines",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AfternoonDose",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "MorningDose",
                table: "Medicines");

            migrationBuilder.DropColumn(
                name: "NightDose",
                table: "Medicines");

            migrationBuilder.AddColumn<string>(
                name: "Dosage",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
