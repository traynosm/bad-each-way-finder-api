using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bad_each_way_finder_api.Migrations
{
    /// <inheritdoc />
    public partial class extendpk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Propositions",
                table: "Propositions");

            migrationBuilder.AlterColumn<string>(
                name: "RunnerName",
                table: "Propositions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "EventId",
                table: "Propositions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Propositions",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Propositions",
                table: "Propositions",
                columns: new[] { "RunnerName", "WinRunnerOddsDecimal", "EventId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Propositions",
                table: "Propositions");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Propositions",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "EventId",
                table: "Propositions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "RunnerName",
                table: "Propositions",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Propositions",
                table: "Propositions",
                column: "Id");
        }
    }
}
