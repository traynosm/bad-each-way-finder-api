using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bad_each_way_finder_api.Migrations
{
    /// <inheritdoc />
    public partial class runners : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Runner_MarketBooks_MarketBookMarketId",
                table: "Runner");

            migrationBuilder.DropForeignKey(
                name: "FK_Runner_StartingPrices_StartingPricesId",
                table: "Runner");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Runner",
                table: "Runner");

            migrationBuilder.RenameTable(
                name: "Runner",
                newName: "Runners");

            migrationBuilder.RenameIndex(
                name: "IX_Runner_StartingPricesId",
                table: "Runners",
                newName: "IX_Runners_StartingPricesId");

            migrationBuilder.RenameIndex(
                name: "IX_Runner_MarketBookMarketId",
                table: "Runners",
                newName: "IX_Runners_MarketBookMarketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Runners",
                table: "Runners",
                column: "SelectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Runners_MarketBooks_MarketBookMarketId",
                table: "Runners",
                column: "MarketBookMarketId",
                principalTable: "MarketBooks",
                principalColumn: "MarketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Runners_StartingPrices_StartingPricesId",
                table: "Runners",
                column: "StartingPricesId",
                principalTable: "StartingPrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Runners_MarketBooks_MarketBookMarketId",
                table: "Runners");

            migrationBuilder.DropForeignKey(
                name: "FK_Runners_StartingPrices_StartingPricesId",
                table: "Runners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Runners",
                table: "Runners");

            migrationBuilder.RenameTable(
                name: "Runners",
                newName: "Runner");

            migrationBuilder.RenameIndex(
                name: "IX_Runners_StartingPricesId",
                table: "Runner",
                newName: "IX_Runner_StartingPricesId");

            migrationBuilder.RenameIndex(
                name: "IX_Runners_MarketBookMarketId",
                table: "Runner",
                newName: "IX_Runner_MarketBookMarketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Runner",
                table: "Runner",
                column: "SelectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Runner_MarketBooks_MarketBookMarketId",
                table: "Runner",
                column: "MarketBookMarketId",
                principalTable: "MarketBooks",
                principalColumn: "MarketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Runner_StartingPrices_StartingPricesId",
                table: "Runner",
                column: "StartingPricesId",
                principalTable: "StartingPrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
