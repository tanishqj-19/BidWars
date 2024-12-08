using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PerformanceReports",
                columns: new[] { "ReportId", "AnalystId", "MatchDate", "MatchType", "Opponent", "PlayerId", "Rating", "Sport", "Stats1", "Stats2", "Stats3", "Stats4", "Tournament" },
                values: new object[,]
                {
                    { 70, 11, new DateTime(2023, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Knockout", "Real Madrid", 21, 8.5, "Football", 1, 2, 50, 3, "UEFA Champions League" },
                    { 71, 12, new DateTime(2023, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Barcelona", 21, 7.7999999999999998, "Football", 0, 1, 45, 4, "La Liga" },
                    { 72, 13, new DateTime(2023, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Semifinal", "Atletico Madrid", 21, 8.3000000000000007, "Football", 1, 1, 55, 2, "Copa del Rey" },
                    { 73, 11, new DateTime(2023, 4, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Sevilla", 21, 9.0, "Football", 2, 0, 60, 5, "La Liga" },
                    { 74, 12, new DateTime(2023, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Final", "Chelsea", 21, 8.6999999999999993, "Football", 1, 1, 48, 3, "UEFA Super Cup" },
                    { 75, 13, new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Manchester United", 22, 8.5, "Football", 0, 0, 40, 10, "Premier League" },
                    { 76, 11, new DateTime(2023, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Knockout", "Real Madrid", 22, 8.1999999999999993, "Football", 0, 0, 42, 9, "UEFA Champions League" },
                    { 77, 12, new DateTime(2023, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quarterfinal", "Chelsea", 22, 8.6999999999999993, "Football", 0, 0, 45, 12, "FA Cup" },
                    { 78, 13, new DateTime(2023, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Tottenham", 22, 9.0, "Football", 1, 0, 50, 8, "Premier League" },
                    { 79, 11, new DateTime(2023, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Final", "Sevilla", 22, 8.8000000000000007, "Football", 0, 0, 55, 7, "UEFA Europa League" },
                    { 80, 12, new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Real Madrid", 23, 8.4000000000000004, "Football", 1, 3, 50, 2, "La Liga" },
                    { 81, 13, new DateTime(2023, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Semifinal", "Atletico Madrid", 23, 8.0999999999999996, "Football", 0, 2, 40, 3, "Copa del Rey" },
                    { 82, 11, new DateTime(2023, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Quarterfinal", "Bayern Munich", 23, 8.5999999999999996, "Football", 1, 1, 48, 2, "UEFA Champions League" },
                    { 83, 12, new DateTime(2023, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Valencia", 23, 9.0, "Football", 2, 0, 55, 1, "La Liga" },
                    { 84, 13, new DateTime(2023, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Final", "Inter Milan", 23, 8.9000000000000004, "Football", 1, 2, 47, 2, "UEFA Super Cup" },
                    { 85, 11, new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Manchester United", 24, 8.8000000000000007, "Football", 2, 1, 35, 5, "Premier League" },
                    { 86, 12, new DateTime(2023, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Semifinal", "Liverpool", 24, 8.5, "Football", 0, 3, 40, 4, "FA Cup" },
                    { 87, 13, new DateTime(2023, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Final", "Arsenal", 24, 8.6999999999999993, "Football", 1, 2, 42, 3, "Carabao Cup" },
                    { 88, 11, new DateTime(2023, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Chelsea", 24, 9.0999999999999996, "Football", 2, 1, 45, 2, "Premier League" },
                    { 89, 12, new DateTime(2023, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Final", "Sevilla", 24, 8.9000000000000004, "Football", 1, 2, 50, 1, "UEFA Europa League" },
                    { 90, 13, new DateTime(2023, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Barcelona", 25, 9.3000000000000007, "Football", 2, 0, 60, 2, "La Liga" },
                    { 91, 11, new DateTime(2023, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "Semifinal", "Manchester City", 25, 8.6999999999999993, "Football", 1, 1, 58, 3, "UEFA Champions League" },
                    { 92, 12, new DateTime(2023, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Final", "Valencia", 25, 9.5, "Football", 2, 2, 62, 4, "Copa del Rey" },
                    { 93, 13, new DateTime(2023, 4, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Sevilla", 25, 8.9000000000000004, "Football", 1, 1, 54, 2, "La Liga" },
                    { 94, 11, new DateTime(2023, 5, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Final", "PSG", 25, 9.6999999999999993, "Football", 3, 1, 68, 1, "UEFA Super Cup" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 70);

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

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 76);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 77);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 78);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 79);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 80);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 81);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 82);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 83);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 84);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 85);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 86);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 87);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 88);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 89);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 90);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 91);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 92);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 93);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 94);
        }
    }
}
