using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bad_each_way_finder_api.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Competitions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timezone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OpenDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarketDescriptions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsPersistenceEnabled = table.Column<bool>(type: "bit", nullable: false),
                    IsBspMarket = table.Column<bool>(type: "bit", nullable: false),
                    MarketTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SuspendTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SettleTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BettingType = table.Column<int>(type: "int", nullable: false),
                    IsTurnInPlayEnabled = table.Column<bool>(type: "bit", nullable: false),
                    MarketType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Regulator = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MarketBaseRate = table.Column<double>(type: "float", nullable: false),
                    IsDiscountAllowed = table.Column<bool>(type: "bit", nullable: false),
                    Wallet = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rules = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RulesHasDate = table.Column<bool>(type: "bit", nullable: false),
                    Clarifications = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MarketDetails",
                columns: table => new
                {
                    marketId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    eventId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    marketName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    marketType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    marketStartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    inplay = table.Column<bool>(type: "bit", nullable: false),
                    livePriceAvailable = table.Column<bool>(type: "bit", nullable: false),
                    guaranteedPriceAvailable = table.Column<bool>(type: "bit", nullable: false),
                    eachwayAvailable = table.Column<bool>(type: "bit", nullable: false),
                    numberOfPlaces = table.Column<int>(type: "int", nullable: false),
                    placeFractionNumerator = table.Column<int>(type: "int", nullable: false),
                    placeFractionDenominator = table.Column<int>(type: "int", nullable: false),
                    linkedMarketId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    rampMarketId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    marketStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketDetails", x => x.marketId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MarketCatalogues",
                columns: table => new
                {
                    MarketId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MarketName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsMarketDataDelayed = table.Column<bool>(type: "bit", nullable: false),
                    DescriptionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EventTypeId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EventId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CompetitionId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketCatalogues", x => x.MarketId);
                    table.ForeignKey(
                        name: "FK_MarketCatalogues_Competitions_CompetitionId",
                        column: x => x.CompetitionId,
                        principalTable: "Competitions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MarketCatalogues_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalTable: "EventTypes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MarketCatalogues_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MarketCatalogues_MarketDescriptions_DescriptionId",
                        column: x => x.DescriptionId,
                        principalTable: "MarketDescriptions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Rule4Deductions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    deduction = table.Column<double>(type: "float", nullable: false),
                    timeFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    timeTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    deductionPriceType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketDetailmarketId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rule4Deductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rule4Deductions_MarketDetails_MarketDetailmarketId",
                        column: x => x.MarketDetailmarketId,
                        principalTable: "MarketDetails",
                        principalColumn: "marketId");
                });

            migrationBuilder.CreateTable(
                name: "RunnerDetails",
                columns: table => new
                {
                    selectionId = table.Column<int>(type: "int", nullable: false),
                    selectionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    runnerOrder = table.Column<int>(type: "int", nullable: false),
                    handicap = table.Column<double>(type: "float", nullable: false),
                    runnerStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MarketDetailmarketId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunnerDetails", x => x.selectionId);
                    table.ForeignKey(
                        name: "FK_RunnerDetails_MarketDetails_MarketDetailmarketId",
                        column: x => x.MarketDetailmarketId,
                        principalTable: "MarketDetails",
                        principalColumn: "marketId");
                });

            migrationBuilder.CreateTable(
                name: "RunnerDescriptions",
                columns: table => new
                {
                    SelectionId = table.Column<long>(type: "bigint", nullable: false),
                    RunnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Handicap = table.Column<double>(type: "float", nullable: false),
                    SortPriority = table.Column<int>(type: "int", nullable: false),
                    MarketCatalogueMarketId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RunnerDescriptions", x => x.SelectionId);
                    table.ForeignKey(
                        name: "FK_RunnerDescriptions_MarketCatalogues_MarketCatalogueMarketId",
                        column: x => x.MarketCatalogueMarketId,
                        principalTable: "MarketCatalogues",
                        principalColumn: "MarketId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MarketCatalogues_CompetitionId",
                table: "MarketCatalogues",
                column: "CompetitionId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketCatalogues_DescriptionId",
                table: "MarketCatalogues",
                column: "DescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketCatalogues_EventId",
                table: "MarketCatalogues",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_MarketCatalogues_EventTypeId",
                table: "MarketCatalogues",
                column: "EventTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Rule4Deductions_MarketDetailmarketId",
                table: "Rule4Deductions",
                column: "MarketDetailmarketId");

            migrationBuilder.CreateIndex(
                name: "IX_RunnerDescriptions_MarketCatalogueMarketId",
                table: "RunnerDescriptions",
                column: "MarketCatalogueMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_RunnerDetails_MarketDetailmarketId",
                table: "RunnerDetails",
                column: "MarketDetailmarketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Rule4Deductions");

            migrationBuilder.DropTable(
                name: "RunnerDescriptions");

            migrationBuilder.DropTable(
                name: "RunnerDetails");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "MarketCatalogues");

            migrationBuilder.DropTable(
                name: "MarketDetails");

            migrationBuilder.DropTable(
                name: "Competitions");

            migrationBuilder.DropTable(
                name: "EventTypes");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "MarketDescriptions");
        }
    }
}
