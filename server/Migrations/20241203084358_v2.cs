using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PerformanceStats",
                table: "Players");

            migrationBuilder.RenameColumn(
                name: "Skills",
                table: "Players",
                newName: "link");

            migrationBuilder.RenameColumn(
                name: "PerformanceDetails",
                table: "PerformanceReports",
                newName: "Sport");

            migrationBuilder.AddColumn<string>(
                name: "MatchType",
                table: "PerformanceReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Opponent",
                table: "PerformanceReports",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Stats1",
                table: "PerformanceReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stats2",
                table: "PerformanceReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stats3",
                table: "PerformanceReports",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stats4",
                table: "PerformanceReports",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MatchType",
                table: "PerformanceReports");

            migrationBuilder.DropColumn(
                name: "Opponent",
                table: "PerformanceReports");

            migrationBuilder.DropColumn(
                name: "Stats1",
                table: "PerformanceReports");

            migrationBuilder.DropColumn(
                name: "Stats2",
                table: "PerformanceReports");

            migrationBuilder.DropColumn(
                name: "Stats3",
                table: "PerformanceReports");

            migrationBuilder.DropColumn(
                name: "Stats4",
                table: "PerformanceReports");

            migrationBuilder.RenameColumn(
                name: "link",
                table: "Players",
                newName: "Skills");

            migrationBuilder.RenameColumn(
                name: "Sport",
                table: "PerformanceReports",
                newName: "PerformanceDetails");

            migrationBuilder.AddColumn<string>(
                name: "PerformanceStats",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
