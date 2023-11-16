using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bad_each_way_finder_api.Migrations
{
    /// <inheritdoc />
    public partial class market_books : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MarketBooks",
                columns: table => new
                {
                    MarketId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsMarketDataDelayed = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    BetDelay = table.Column<int>(type: "int", nullable: false),
                    IsBspReconciled = table.Column<bool>(type: "bit", nullable: false),
                    IsComplete = table.Column<bool>(type: "bit", nullable: false),
                    IsInplay = table.Column<bool>(type: "bit", nullable: false),
                    NumberOfWinners = table.Column<int>(type: "int", nullable: false),
                    NumberOfRunners = table.Column<int>(type: "int", nullable: false),
                    NumberOfActiveRunners = table.Column<int>(type: "int", nullable: false),
                    LastMatchTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TotalMatched = table.Column<double>(type: "float", nullable: false),
                    TotalAvailable = table.Column<double>(type: "float", nullable: false),
                    IsCrossMatching = table.Column<bool>(type: "bit", nullable: false),
                    IsRunnersVoidable = table.Column<bool>(type: "bit", nullable: false),
                    Version = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketBooks", x => x.MarketId);
                });

            migrationBuilder.CreateTable(
                name: "StartingPrices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NearPrice = table.Column<double>(type: "float", nullable: false),
                    FarPrice = table.Column<double>(type: "float", nullable: false),
                    ActualSP = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StartingPrices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Runner",
                columns: table => new
                {
                    SelectionId = table.Column<long>(type: "bigint", nullable: false),
                    Handicap = table.Column<double>(type: "float", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AdjustmentFactor = table.Column<double>(type: "float", nullable: true),
                    LastPriceTraded = table.Column<double>(type: "float", nullable: true),
                    TotalMatched = table.Column<double>(type: "float", nullable: false),
                    RemovalDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StartingPricesId = table.Column<int>(type: "int", nullable: false),
                    MarketBookMarketId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Runner", x => x.SelectionId);
                    table.ForeignKey(
                        name: "FK_Runner_MarketBooks_MarketBookMarketId",
                        column: x => x.MarketBookMarketId,
                        principalTable: "MarketBooks",
                        principalColumn: "MarketId");
                    table.ForeignKey(
                        name: "FK_Runner_StartingPrices_StartingPricesId",
                        column: x => x.StartingPricesId,
                        principalTable: "StartingPrices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Runner_MarketBookMarketId",
                table: "Runner",
                column: "MarketBookMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_Runner_StartingPricesId",
                table: "Runner",
                column: "StartingPricesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Runner");

            migrationBuilder.DropTable(
                name: "MarketBooks");

            migrationBuilder.DropTable(
                name: "StartingPrices");
        }
    }
}
