using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bad_each_way_finder_api.Migrations
{
    /// <inheritdoc />
    public partial class remove_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "Propositions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "Propositions",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
