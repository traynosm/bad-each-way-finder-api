using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bad_each_way_finder_api.Migrations
{
    /// <inheritdoc />
    public partial class runner_info_each_way_place_parts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "EachWayPlacePart",
                table: "Propositions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EachWayPlacePart",
                table: "Propositions");
        }
    }
}
