using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace server.Migrations
{
    /// <inheritdoc />
    public partial class seedPlayers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "link",
                table: "Players");

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "PlayerId", "Age", "AgentId", "BasePrice", "Country", "Name", "Position", "Sport", "Status", "TeamId" },
                values: new object[,]
                {
                    { 2, 36, 2, 180000m, "India", "Rohit Sharma", "Batsman", "Cricket", "Available", null },
                    { 3, 33, 3, 190000m, "Australia", "Steve Smith", "Batsman", "Cricket", "Available", null },
                    { 4, 33, 4, 185000m, "New Zealand", "Kane Williamson", "Batsman", "Cricket", "Available", null },
                    { 5, 30, 5, 150000m, "India", "Jasprit Bumrah", "Bowler", "Cricket", "Available", null },
                    { 7, 34, 2, 155000m, "New Zealand", "Trent Boult", "Bowler", "Cricket", "Available", null },
                    { 8, 25, 3, 140000m, "Afghanistan", "Rashid Khan", "Bowler", "Cricket", "Available", null },
                    { 9, 32, 4, 175000m, "England", "Jos Buttler", "Wicketkeeper-Batsman", "Cricket", "Available", null },
                    { 10, 41, 5, 190000m, "India", "MS Dhoni", "Wicketkeeper-Batsman", "Cricket", "Available", null },
                    { 12, 35, 2, 180000m, "India", "Ravindra Jadeja", "All-Rounder", "Cricket", "Available", null },
                    { 13, 30, 3, 170000m, "Australia", "Pat Cummins", "All-Rounder", "Cricket", "Available", null },
                    { 14, 36, 4, 300000m, "Argentina", "Lionel Messi", "Forward", "Football", "Available", null },
                    { 15, 38, 5, 290000m, "Portugal", "Cristiano Ronaldo", "Forward", "Football", "Available", null },
                    { 17, 32, 2, 270000m, "Belgium", "Kevin De Bruyne", "Midfielder", "Football", "Available", null },
                    { 18, 25, 3, 295000m, "France", "Kylian Mbappé", "Forward", "Football", "Available", null },
                    { 19, 35, 4, 280000m, "Poland", "Robert Lewandowski", "Forward", "Football", "Available", null },
                    { 20, 23, 5, 320000m, "Norway", "Erling Haaland", "Forward", "Football", "Available", null },
                    { 22, 32, 2, 270000m, "Netherlands", "Virgil van Dijk", "Defender", "Football", "Available", null },
                    { 23, 21, 3, 240000m, "Spain", "Pedri González", "Midfielder", "Football", "Available", null },
                    { 24, 22, 4, 250000m, "England", "Cole Palmer", "Midfielder", "Football", "Available", null },
                    { 25, 24, 5, 300000m, "Brazil", "Vinicious Junior", "Forward", "Football", "Available", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Email", "IsActive", "Password", "Role", "Username" },
                values: new object[] { 1, "davidagent@gmail.com", true, "$2a$10$f8g/nLj9O/SibpWRhz3GjOJe4zXPvkGkz7k609g7BZXyntrcOP5By", "Player Agent", "David Agent" });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "PlayerId", "Age", "AgentId", "BasePrice", "Country", "Name", "Position", "Sport", "Status", "TeamId" },
                values: new object[,]
                {
                    { 1, 34, 1, 200000m, "India", "Virat Kohli", "Batsman", "Cricket", "Available", null },
                    { 6, 33, 1, 160000m, "Australia", "Mitchell Starc", "Bowler", "Cricket", "Available", null },
                    { 11, 32, 1, 200000m, "England", "Ben Stokes", "All-Rounder", "Cricket", "Available", null },
                    { 16, 31, 1, 280000m, "Brazil", "Neymar Jr.", "Forward", "Football", "Available", null },
                    { 21, 38, 1, 250000m, "Croatia", "Luka Modrić", "Midfielder", "Football", "Available", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "PlayerId",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "link",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
