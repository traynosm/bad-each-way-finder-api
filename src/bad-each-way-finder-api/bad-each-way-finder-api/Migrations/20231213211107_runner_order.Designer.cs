﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using bad_each_way_finder_api.Areas.Identity.Data;

#nullable disable

namespace bad_each_way_finder_api.Migrations
{
    [DbContext(typeof(BadEachWayFinderApiContext))]
    [Migration("20231213211107_runner_order")]
    partial class runner_order
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.DomainModel.Proposition", b =>
                {
                    b.Property<string>("RunnerName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("WinRunnerOddsDecimal")
                        .HasColumnType("float");

                    b.Property<string>("EventId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("EachWayExpectedValue")
                        .HasColumnType("float");

                    b.Property<double>("EachWayPlacePart")
                        .HasColumnType("float");

                    b.Property<DateTime>("EventDateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("EventName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExchangePlaceMarketId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ExchangePlacePrice")
                        .HasColumnType("float");

                    b.Property<string>("ExchangeWinMarketId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ExchangeWinPrice")
                        .HasColumnType("float");

                    b.Property<double>("PlaceExpectedValue")
                        .HasColumnType("float");

                    b.Property<DateTime>("RecordedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("RunnerOrder")
                        .HasColumnType("int");

                    b.Property<long>("RunnerSelectionId")
                        .HasColumnType("bigint");

                    b.Property<bool>("SportsbookEachwayAvailable")
                        .HasColumnType("bit");

                    b.Property<int>("SportsbookNumberOfPlaces")
                        .HasColumnType("int");

                    b.Property<int>("SportsbookPlaceFractionDenominator")
                        .HasColumnType("int");

                    b.Property<string>("SportsbookWinMarketId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("WinExpectedValue")
                        .HasColumnType("float");

                    b.HasKey("RunnerName", "WinRunnerOddsDecimal", "EventId");

                    b.ToTable("Propositions");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Exchange.Competition", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Competitions");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Exchange.Event", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CountryCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("OpenDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Timezone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Venue")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Exchange.EventType", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("EventTypes");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Exchange.MarketBook", b =>
                {
                    b.Property<string>("MarketId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BetDelay")
                        .HasColumnType("int");

                    b.Property<bool>("IsBspReconciled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsComplete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsCrossMatching")
                        .HasColumnType("bit");

                    b.Property<bool>("IsInplay")
                        .HasColumnType("bit");

                    b.Property<bool>("IsMarketDataDelayed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsRunnersVoidable")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LastMatchTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfActiveRunners")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfRunners")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfWinners")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<double>("TotalAvailable")
                        .HasColumnType("float");

                    b.Property<double>("TotalMatched")
                        .HasColumnType("float");

                    b.Property<long>("Version")
                        .HasColumnType("bigint");

                    b.HasKey("MarketId");

                    b.ToTable("MarketBooks");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Exchange.MarketCatalogue", b =>
                {
                    b.Property<string>("MarketId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CompetitionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("DescriptionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EventId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EventTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("IsMarketDataDelayed")
                        .HasColumnType("bit");

                    b.Property<string>("MarketName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("MarketId");

                    b.HasIndex("CompetitionId");

                    b.HasIndex("DescriptionId");

                    b.HasIndex("EventId");

                    b.HasIndex("EventTypeId");

                    b.ToTable("MarketCatalogues");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Exchange.MarketDescription", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("BettingType")
                        .HasColumnType("int");

                    b.Property<string>("Clarifications")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsBspMarket")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDiscountAllowed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPersistenceEnabled")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTurnInPlayEnabled")
                        .HasColumnType("bit");

                    b.Property<double>("MarketBaseRate")
                        .HasColumnType("float");

                    b.Property<DateTime>("MarketTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("MarketType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Regulator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Rules")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RulesHasDate")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("SettleTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("SuspendTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Wallet")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("MarketDescriptions");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Exchange.Runner", b =>
                {
                    b.Property<long>("SelectionId")
                        .HasColumnType("bigint");

                    b.Property<double?>("AdjustmentFactor")
                        .HasColumnType("float");

                    b.Property<double?>("Handicap")
                        .HasColumnType("float");

                    b.Property<double?>("LastPriceTraded")
                        .HasColumnType("float");

                    b.Property<string>("MarketBookMarketId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("RemovalDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StartingPricesId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<double>("TotalMatched")
                        .HasColumnType("float");

                    b.HasKey("SelectionId");

                    b.HasIndex("MarketBookMarketId");

                    b.HasIndex("StartingPricesId");

                    b.ToTable("Runners");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Exchange.RunnerDescription", b =>
                {
                    b.Property<long>("SelectionId")
                        .HasColumnType("bigint");

                    b.Property<double>("Handicap")
                        .HasColumnType("float");

                    b.Property<string>("MarketCatalogueMarketId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RunnerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SortPriority")
                        .HasColumnType("int");

                    b.HasKey("SelectionId");

                    b.HasIndex("MarketCatalogueMarketId");

                    b.ToTable("RunnerDescriptions");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Exchange.StartingPrices", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("ActualSP")
                        .HasColumnType("float");

                    b.Property<double>("FarPrice")
                        .HasColumnType("float");

                    b.Property<double>("NearPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("StartingPrices");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Sportsbook.MarketDetail", b =>
                {
                    b.Property<string>("marketId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("eachwayAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("eventId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("guaranteedPriceAvailable")
                        .HasColumnType("bit");

                    b.Property<bool>("inplay")
                        .HasColumnType("bit");

                    b.Property<string>("linkedMarketId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("livePriceAvailable")
                        .HasColumnType("bit");

                    b.Property<string>("marketName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("marketStartTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("marketStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("marketType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("numberOfPlaces")
                        .HasColumnType("int");

                    b.Property<int>("placeFractionDenominator")
                        .HasColumnType("int");

                    b.Property<int>("placeFractionNumerator")
                        .HasColumnType("int");

                    b.Property<string>("rampMarketId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("marketId");

                    b.ToTable("MarketDetails");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Sportsbook.Rule4Deduction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("MarketDetailmarketId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("deduction")
                        .HasColumnType("float");

                    b.Property<string>("deductionPriceType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("timeFrom")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("timeTo")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("MarketDetailmarketId");

                    b.ToTable("Rule4Deductions");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Sportsbook.RunnerDetail", b =>
                {
                    b.Property<int>("selectionId")
                        .HasColumnType("int");

                    b.Property<string>("MarketDetailmarketId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<double>("handicap")
                        .HasColumnType("float");

                    b.Property<int>("runnerOrder")
                        .HasColumnType("int");

                    b.Property<string>("runnerStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("selectionName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("selectionId");

                    b.HasIndex("MarketDetailmarketId");

                    b.ToTable("RunnerDetails");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Exchange.MarketCatalogue", b =>
                {
                    b.HasOne("bad_each_way_finder_api_domain.Exchange.Competition", "Competition")
                        .WithMany()
                        .HasForeignKey("CompetitionId");

                    b.HasOne("bad_each_way_finder_api_domain.Exchange.MarketDescription", "Description")
                        .WithMany()
                        .HasForeignKey("DescriptionId");

                    b.HasOne("bad_each_way_finder_api_domain.Exchange.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId");

                    b.HasOne("bad_each_way_finder_api_domain.Exchange.EventType", "EventType")
                        .WithMany()
                        .HasForeignKey("EventTypeId");

                    b.Navigation("Competition");

                    b.Navigation("Description");

                    b.Navigation("Event");

                    b.Navigation("EventType");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Exchange.Runner", b =>
                {
                    b.HasOne("bad_each_way_finder_api_domain.Exchange.MarketBook", null)
                        .WithMany("Runners")
                        .HasForeignKey("MarketBookMarketId");

                    b.HasOne("bad_each_way_finder_api_domain.Exchange.StartingPrices", "StartingPrices")
                        .WithMany()
                        .HasForeignKey("StartingPricesId");

                    b.Navigation("StartingPrices");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Exchange.RunnerDescription", b =>
                {
                    b.HasOne("bad_each_way_finder_api_domain.Exchange.MarketCatalogue", null)
                        .WithMany("Runners")
                        .HasForeignKey("MarketCatalogueMarketId");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Sportsbook.Rule4Deduction", b =>
                {
                    b.HasOne("bad_each_way_finder_api_domain.Sportsbook.MarketDetail", null)
                        .WithMany("rule4Deductions")
                        .HasForeignKey("MarketDetailmarketId");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Sportsbook.RunnerDetail", b =>
                {
                    b.HasOne("bad_each_way_finder_api_domain.Sportsbook.MarketDetail", null)
                        .WithMany("runnerDetails")
                        .HasForeignKey("MarketDetailmarketId");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Exchange.MarketBook", b =>
                {
                    b.Navigation("Runners");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Exchange.MarketCatalogue", b =>
                {
                    b.Navigation("Runners");
                });

            modelBuilder.Entity("bad_each_way_finder_api_domain.Sportsbook.MarketDetail", b =>
                {
                    b.Navigation("rule4Deductions");

                    b.Navigation("runnerDetails");
                });
#pragma warning restore 612, 618
        }
    }
}
