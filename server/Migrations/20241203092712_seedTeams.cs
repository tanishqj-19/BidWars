using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class seedTeams : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "TeamId", "Budget", "ManagerId", "Name", "Region", "RosterSize", "Sport", "TotalExpenditure" },
                values: new object[,]
                {
                    { 1, 1600000m, 1, "Chennai Super Kings", "India", 0, "Cricket", 0m },
                    { 2, 1750000m, 2, "Royal Challengers Bangalore", "India", 0, "Cricket", 0m },
                    { 3, 1550000m, 3, "Mumbai Indians", "India", 0, "Cricket", 0m },
                    { 4, 1600000m, 4, "Real Madrid", "Spain", 0, "Football", 0m },
                    { 5, 1650000m, 5, "Manchester City", "England", 0, "Football", 0m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "TeamId",
                keyValue: 5);
        }
    }
}
