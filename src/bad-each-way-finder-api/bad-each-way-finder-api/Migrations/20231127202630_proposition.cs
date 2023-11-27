using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bad_each_way_finder_api.Migrations
{
    /// <inheritdoc />
    public partial class proposition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Propositions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    EventId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExchangeWinMarketId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExchangePlaceMarketId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SportsbookWinMarketId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SportsbookEachwayAvailable = table.Column<bool>(type: "bit", nullable: false),
                    SportsbookNumberOfPlaces = table.Column<int>(type: "int", nullable: false),
                    SportsbookPlaceFractionDenominator = table.Column<int>(type: "int", nullable: false),
                    RunnerSelectionId = table.Column<long>(type: "bigint", nullable: false),
                    RunnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExchangeWinPrice = table.Column<double>(type: "float", nullable: false),
                    ExchangePlacePrice = table.Column<double>(type: "float", nullable: false),
                    WinRunnerOddsDecimal = table.Column<double>(type: "float", nullable: false),
                    WinExpectedValue = table.Column<double>(type: "float", nullable: false),
                    PlaceExpectedValue = table.Column<double>(type: "float", nullable: false),
                    EachWayExpectedValue = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Propositions", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Propositions");
        }
    }
}
