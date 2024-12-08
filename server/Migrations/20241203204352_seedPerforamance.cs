using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class seedPerforamance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PerformanceReports",
                columns: new[] { "ReportId", "AnalystId", "MatchDate", "MatchType", "Opponent", "PlayerId", "Rating", "Sport", "Stats1", "Stats2", "Stats3", "Stats4", "Tournament" },
                values: new object[,]
                {
                    { 1, 11, new DateTime(2023, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Mumbai Indians", 1, 8.5, "Cricket", 78, 12, 0, 0, "IPL" },
                    { 2, 11, new DateTime(2023, 6, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "Australia", 1, 9.1999999999999993, "Cricket", 112, 25, 0, 0, "World Cup" },
                    { 3, 11, new DateTime(2023, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "England", 1, 7.7999999999999998, "Cricket", 64, 38, 0, 0, "Test Series" },
                    { 4, 11, new DateTime(2023, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "South Africa", 1, 8.6999999999999993, "Cricket", 92, 18, 0, 0, "Bilateral Series" },
                    { 5, 11, new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Pakistan", 1, 7.5, "Cricket", 55, 8, 0, 0, "Asia Cup" },
                    { 6, 12, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Chennai Super Kings", 2, 8.3000000000000007, "Cricket", 82, 15, 0, 0, "IPL" },
                    { 7, 12, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "New Zealand", 2, 9.0, "Cricket", 103, 22, 0, 0, "World Cup" },
                    { 8, 12, new DateTime(2023, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "West Indies", 2, 7.5999999999999996, "Cricket", 58, 35, 0, 0, "Test Series" },
                    { 9, 12, new DateTime(2023, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "Bangladesh", 2, 8.5, "Cricket", 88, 16, 0, 0, "Bilateral Series" },
                    { 10, 12, new DateTime(2023, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Sri Lanka", 2, 7.7000000000000002, "Cricket", 62, 10, 0, 0, "Asia Cup" },
                    { 16, 13, new DateTime(2023, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Royal Challengers Bangalore", 5, 8.1999999999999993, "Cricket", 0, 0, 3, 25, "IPL" },
                    { 17, 13, new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "England", 5, 9.0, "Cricket", 0, 0, 4, 38, "World Cup" },
                    { 18, 13, new DateTime(2023, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "Australia", 5, 8.6999999999999993, "Cricket", 0, 0, 5, 42, "Test Series" },
                    { 19, 13, new DateTime(2023, 8, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "West Indies", 5, 7.9000000000000004, "Cricket", 0, 0, 3, 30, "Bilateral Series" },
                    { 20, 13, new DateTime(2023, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Afghanistan", 5, 7.5, "Cricket", 0, 0, 2, 20, "Asia Cup" },
                    { 51, 11, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Lyon", 14, 8.6999999999999993, "Football", 2, 1, 0, 0, "Ligue 1" },
                    { 52, 11, new DateTime(2023, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cup", "Real Madrid", 14, 8.5, "Football", 1, 2, 0, 0, "Champions League" },
                    { 53, 11, new DateTime(2023, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "International", "Brazil", 14, 9.1999999999999993, "Football", 3, 0, 0, 0, "Friendly" },
                    { 54, 11, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cup", "Barcelona", 14, 8.8000000000000007, "Football", 2, 1, 0, 0, "Supercup" },
                    { 55, 11, new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Marseille", 14, 8.0, "Football", 1, 1, 0, 0, "League" },
                    { 56, 12, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Al Hilal", 15, 8.5, "Football", 2, 0, 0, 0, "Saudi League" },
                    { 57, 12, new DateTime(2023, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cup", "Al Ittihad", 15, 9.0, "Football", 3, 1, 0, 0, "Arab Club Champions Cup" },
                    { 58, 12, new DateTime(2023, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "International", "France", 15, 8.1999999999999993, "Football", 1, 2, 0, 0, "Friendly" },
                    { 59, 12, new DateTime(2023, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cup", "Al Nassr", 15, 8.6999999999999993, "Football", 2, 1, 0, 0, "Saudi Super Cup" },
                    { 60, 12, new DateTime(2023, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Al Shabab", 15, 8.0, "Football", 1, 1, 0, 0, "League" },
                    { 71, 13, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Liverpool", 20, 9.5, "Football", 3, 0, 0, 0, "Premier League" },
                    { 72, 13, new DateTime(2023, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cup", "Real Madrid", 20, 9.0, "Football", 2, 1, 0, 0, "Champions League" },
                    { 73, 13, new DateTime(2023, 7, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "International", "Spain", 20, 8.6999999999999993, "Football", 1, 2, 0, 0, "Friendly" },
                    { 74, 13, new DateTime(2023, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cup", "Arsenal", 20, 9.1999999999999993, "Football", 2, 1, 0, 0, "Community Shield" },
                    { 75, 13, new DateTime(2023, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Chelsea", 20, 9.3000000000000007, "Football", 3, 0, 0, 0, "Premier League" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 71);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 72);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 73);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 74);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 75);
        }
    }
}
