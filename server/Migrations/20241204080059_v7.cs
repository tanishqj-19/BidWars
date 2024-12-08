using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PerformanceReports",
                columns: new[] { "ReportId", "AnalystId", "MatchDate", "MatchType", "Opponent", "PlayerId", "Rating", "Sport", "Stats1", "Stats2", "Stats3", "Stats4", "Tournament" },
                values: new object[,]
                {
                    { 1, 11, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Royal Challengers Bangalore", 1, 8.5, "Cricket", 75, 45, 0, 0, "IPL" },
                    { 2, 12, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "Australia", 1, 9.1999999999999993, "Cricket", 110, 95, 0, 0, "World Cup" },
                    { 3, 13, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "England", 1, 8.0, "Cricket", 85, 180, 0, 0, "Test Series" },
                    { 4, 11, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Mumbai Indians", 2, 7.7999999999999998, "Cricket", 65, 40, 1, 12, "Domestic League" },
                    { 5, 12, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "New Zealand", 2, 8.6999999999999993, "Cricket", 95, 80, 0, 0, "World Cup" },
                    { 6, 13, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "South Africa", 2, 7.5, "Cricket", 70, 150, 0, 0, "Test Series" },
                    { 7, 11, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Sydney Sixers", 3, 7.5, "Cricket", 55, 35, 0, 0, "Big Bash League" },
                    { 8, 12, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "India", 3, 8.3000000000000007, "Cricket", 80, 70, 0, 0, "World Cup" },
                    { 9, 13, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "West Indies", 3, 8.5, "Cricket", 90, 200, 0, 0, "Test Series" },
                    { 10, 11, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Auckland Aces", 4, 8.0, "Cricket", 60, 45, 1, 12, "Super Smash" },
                    { 11, 12, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "Pakistan", 4, 8.5, "Cricket", 85, 75, 0, 0, "World Cup" },
                    { 12, 13, new DateTime(2024, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "Bangladesh", 4, 7.7999999999999998, "Cricket", 75, 180, 0, 0, "Test Series" },
                    { 13, 11, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Chennai Super Kings", 5, 8.6999999999999993, "Cricket", 15, 10, 3, 24, "IPL" },
                    { 14, 12, new DateTime(2024, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "Sri Lanka", 5, 9.0, "Cricket", 10, 8, 2, 60, "World Cup" },
                    { 15, 13, new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "England", 5, 8.5, "Cricket", 20, 15, 4, 120, "Test Series" },
                    { 16, 11, new DateTime(2024, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Sydney Thunder", 6, 8.5, "Cricket", 20, 15, 4, 24, "Big Bash League" },
                    { 17, 12, new DateTime(2024, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "India", 6, 8.6999999999999993, "Cricket", 15, 10, 3, 60, "World Cup" },
                    { 18, 13, new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "England", 6, 9.0, "Cricket", 25, 20, 5, 120, "Test Series" },
                    { 19, 11, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Canterbury Kings", 7, 8.0, "Cricket", 15, 12, 3, 24, "Super Smash" },
                    { 20, 12, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "Pakistan", 7, 8.3000000000000007, "Cricket", 10, 8, 2, 55, "World Cup" },
                    { 21, 13, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "South Africa", 7, 8.5, "Cricket", 20, 15, 4, 110, "Test Series" },
                    { 22, 11, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Kabul Zwanan", 8, 8.6999999999999993, "Cricket", 35, 25, 3, 24, "Afghanistan Premier League" },
                    { 23, 12, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "Sri Lanka", 8, 8.5, "Cricket", 25, 20, 2, 48, "World Cup" },
                    { 24, 13, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "Bangladesh", 8, 9.0, "Cricket", 40, 30, 4, 96, "Test Series" },
                    { 25, 11, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Rajasthan Royals", 9, 8.8000000000000007, "Cricket", 80, 50, 0, 0, "IPL" },
                    { 26, 12, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "Australia", 9, 9.1999999999999993, "Cricket", 105, 85, 0, 0, "World Cup" },
                    { 27, 13, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "West Indies", 9, 8.0, "Cricket", 75, 160, 0, 0, "Test Series" },
                    { 28, 11, new DateTime(2024, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Chennai Super Kings", 10, 8.3000000000000007, "Cricket", 50, 35, 0, 0, "IPL" },
                    { 29, 12, new DateTime(2024, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "Mumbai Indians", 10, 8.5, "Cricket", 65, 55, 0, 0, "Domestic Match" },
                    { 30, 13, new DateTime(2024, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "India Maharajas", 10, 8.0, "Cricket", 45, 30, 0, 0, "Legends League" },
                    { 31, 11, new DateTime(2024, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Manchester Originals", 11, 9.0, "Cricket", 65, 40, 3, 24, "The Hundred" },
                    { 32, 12, new DateTime(2024, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "Australia", 11, 9.1999999999999993, "Cricket", 85, 70, 2, 48, "World Cup" },
                    { 33, 13, new DateTime(2024, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "India", 11, 8.8000000000000007, "Cricket", 75, 160, 4, 120, "Test Series" },
                    { 34, 11, new DateTime(2024, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Chennai Super Kings", 12, 8.5, "Cricket", 45, 30, 2, 24, "IPL" },
                    { 35, 12, new DateTime(2024, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "Pakistan", 12, 8.6999999999999993, "Cricket", 55, 40, 3, 48, "World Cup" },
                    { 36, 13, new DateTime(2024, 3, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "England", 12, 8.3000000000000007, "Cricket", 40, 80, 4, 96, "Test Series" },
                    { 37, 11, new DateTime(2024, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "T20", "Sydney Sixers", 13, 8.5999999999999996, "Cricket", 25, 15, 4, 24, "Big Bash League" },
                    { 38, 12, new DateTime(2024, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "ODI", "New Zealand", 13, 8.8000000000000007, "Cricket", 35, 25, 3, 60, "World Cup" },
                    { 39, 13, new DateTime(2024, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Test", "South Africa", 13, 9.0, "Cricket", 40, 30, 5, 120, "Test Series" },
                    { 40, 11, new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Inter Miami", 14, 9.0, "Football", 2, 1, 35, 2, "Leagues Cup" },
                    { 41, 12, new DateTime(2024, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "International", "Brazil", 14, 8.6999999999999993, "Football", 1, 2, 45, 1, "Coppa America" },
                    { 42, 13, new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cup", "Real Madrid", 14, 8.5, "Football", 1, 1, 40, 3, "Champions League" },
                    { 43, 11, new DateTime(2024, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Al Nassr", 15, 8.8000000000000007, "Football", 3, 0, 25, 1, "Saudi Pro League" },
                    { 44, 12, new DateTime(2024, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "International Cup", "Al Hilal", 15, 9.0, "Football", 2, 1, 30, 2, "Club World Cup" },
                    { 45, 13, new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Al Ittihad", 15, 8.5, "Football", 1, 2, 35, 1, "Saudi Pro League" },
                    { 46, 11, new DateTime(2024, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Paris Saint-Germain", 16, 8.6999999999999993, "Football", 2, 1, 40, 2, "Ligue 1" },
                    { 47, 12, new DateTime(2024, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "International Cup", "Flamengo", 16, 8.5, "Football", 1, 2, 45, 1, "Copa Libertadores" },
                    { 48, 13, new DateTime(2024, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cup", "Barcelona", 16, 8.3000000000000007, "Football", 1, 1, 38, 3, "Champions League" },
                    { 49, 11, new DateTime(2024, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Manchester City", 17, 8.9000000000000004, "Football", 0, 3, 55, 2, "Premier League" },
                    { 50, 12, new DateTime(2024, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cup", "Chelsea", 17, 8.6999999999999993, "Football", 1, 2, 48, 1, "FA Cup" },
                    { 51, 13, new DateTime(2024, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cup", "Real Madrid", 17, 8.5, "Football", 1, 1, 50, 3, "Champions League" },
                    { 52, 11, new DateTime(2024, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Paris Saint-Germain", 18, 9.0, "Football", 2, 1, 35, 1, "Ligue 1" },
                    { 53, 12, new DateTime(2024, 2, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cup", "Barcelona", 18, 8.8000000000000007, "Football", 1, 2, 40, 2, "Champions League" },
                    { 54, 13, new DateTime(2024, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cup", "Lyon", 18, 9.1999999999999993, "Football", 3, 0, 30, 1, "Coupe de France" },
                    { 55, 11, new DateTime(2024, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Barcelona", 19, 8.5999999999999996, "Football", 2, 1, 25, 1, "La Liga" },
                    { 56, 12, new DateTime(2024, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cup", "Real Madrid", 19, 8.4000000000000004, "Football", 1, 1, 30, 2, "Copa del Rey" },
                    { 57, 13, new DateTime(2024, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cup", "Manchester City", 19, 8.5, "Football", 1, 2, 28, 1, "Champions League" },
                    { 58, 11, new DateTime(2024, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "League", "Manchester United", 20, 9.3000000000000007, "Football", 3, 0, 20, 1, "Premier League" },
                    { 59, 12, new DateTime(2024, 2, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cup", "Chelsea", 20, 9.0999999999999996, "Football", 2, 1, 25, 2, "FA Cup" },
                    { 60, 13, new DateTime(2024, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cup", "Real Madrid", 20, 8.9000000000000004, "Football", 1, 2, 30, 1, "Champions League" }
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
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 15);

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
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 33);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 35);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 36);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 37);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 38);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 39);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 40);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 41);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 42);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 43);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 44);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 45);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 46);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 47);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 48);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "PerformanceReports",
                keyColumn: "ReportId",
                keyValue: 50);

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
        }
    }
}
