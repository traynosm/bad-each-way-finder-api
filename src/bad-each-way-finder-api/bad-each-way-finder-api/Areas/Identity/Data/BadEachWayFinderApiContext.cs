using bad_each_way_finder_api_domain.DomainModel;
using bad_each_way_finder_api_domain.Exchange;
using bad_each_way_finder_api_domain.Sportsbook;
using bad_each_way_finder_domain.DomainModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace bad_each_way_finder_api.Areas.Identity.Data;

public class BadEachWayFinderApiContext : IdentityDbContext<IdentityUser>
{
    public BadEachWayFinderApiContext(DbContextOptions<BadEachWayFinderApiContext> options)
        : base(options)
    {
    }

    //domain
    public DbSet<Proposition> Propositions { get; set; }
    public DbSet<Race> Races { get; set; }
    public DbSet<RunnerInfo> RunnerInfos { get; set; }

    //common
    public DbSet<Competition> Competitions { get; set; }
    public DbSet<EventType> EventTypes { get; set; }
    public DbSet<Event> Events { get; set; }

    //exchange
    public DbSet<MarketCatalogue> MarketCatalogues { get; set; }
    public DbSet<MarketDescription> MarketDescriptions { get; set; }
    public DbSet<RunnerDescription> RunnerDescriptions { get; set; }
    public DbSet<MarketBook> MarketBooks { get; set; }  
    public DbSet<Runner> Runners { get; set; }

    //sportsbook
    public DbSet<MarketDetail> MarketDetails { get; set; }
    public DbSet<RunnerDetail> RunnerDetails { get; set; }
    public DbSet<Rule4Deduction> Rule4Deductions { get; set; }
    public DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<MarketDescription>().Property(b => b.Id)
           .ValueGeneratedOnAdd();

        builder.Entity<Proposition>()
            .HasKey(nameof(Proposition.RunnerName), 
                    nameof(Proposition.WinRunnerOddsDecimal),
                    nameof(Proposition.EventId));

        base.OnModelCreating(builder);
    }
}
