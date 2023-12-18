using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bad_each_way_finder_api.Migrations
{
    /// <inheritdoc />
    public partial class add_latest_prices_propositon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RunnerStatus",
                table: "RunnerInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdated",
                table: "Races",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<double>(
                name: "LatestPlacePrice",
                table: "Propositions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "LatestWinPrice",
                table: "Propositions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "RunnerStatus",
                table: "Propositions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RunnerStatus",
                table: "RunnerInfos");

            migrationBuilder.DropColumn(
                name: "LastUpdated",
                table: "Races");

            migrationBuilder.DropColumn(
                name: "LatestPlacePrice",
                table: "Propositions");

            migrationBuilder.DropColumn(
                name: "LatestWinPrice",
                table: "Propositions");

            migrationBuilder.DropColumn(
                name: "RunnerStatus",
                table: "Propositions");
        }
    }
}
