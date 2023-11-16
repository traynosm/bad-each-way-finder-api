using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bad_each_way_finder_api.Migrations
{
    /// <inheritdoc />
    public partial class sp_nullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Runners_StartingPrices_StartingPricesId",
                table: "Runners");

            migrationBuilder.AlterColumn<int>(
                name: "StartingPricesId",
                table: "Runners",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Runners_StartingPrices_StartingPricesId",
                table: "Runners",
                column: "StartingPricesId",
                principalTable: "StartingPrices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Runners_StartingPrices_StartingPricesId",
                table: "Runners");

            migrationBuilder.AlterColumn<int>(
                name: "StartingPricesId",
                table: "Runners",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Runners_StartingPrices_StartingPricesId",
                table: "Runners",
                column: "StartingPricesId",
                principalTable: "StartingPrices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
