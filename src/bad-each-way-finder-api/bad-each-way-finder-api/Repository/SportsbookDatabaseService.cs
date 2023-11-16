using bad_each_way_finder_api.Areas.Identity.Data;
using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.Sportsbook;
using Microsoft.EntityFrameworkCore;

namespace bad_each_way_finder_api.Repository
{
    public class SportsbookDatabaseService : DatabaseService, ISportsbookDatabaseService
    {
        public SportsbookDatabaseService(BadEachWayFinderApiContext context, ILogger<DatabaseService> logger) :
            base(context, logger)
        {

        }

        public void AddOrUpdateMarketDetails(MarketDetails marketDetails, bool clearData = true) 
        {
            if (clearData && _context.MarketDetails.Any(m => m.marketStartTime.Date == DateTime.Today))
            {
                DeleteContent();
            }

            foreach (var marketDetail in marketDetails.marketDetails)
            {
                var savedMarketDetail = _context.MarketDetails.Find(marketDetail.marketId);

                if (savedMarketDetail != null)
                {
                    _context.Entry(savedMarketDetail).CurrentValues.SetValues(marketDetail);
                }
                else
                {
                    var savedRunners = new List<RunnerDetail>();
                    foreach (var runner in marketDetail.runnerDetails)
                    {
                        var savedRunner = _context.RunnerDetails.Find(runner.selectionId);

                        if (savedRunner != null)
                        {
                            savedRunners.Add(savedRunner);
                        }
                        else
                        {
                            _context.RunnerDetails.Add(runner);
                            savedRunners.Add(runner);
                        }
                    }

                    marketDetail.runnerDetails = savedRunners;

                    var savedRule4Deductions = new List<Rule4Deduction>();
                    foreach (var rule4 in marketDetail.rule4Deductions)
                    {
                        var savedRule4 = _context.Rule4Deductions.Find(rule4.Id);

                        if (savedRule4 != null)
                        {
                            savedRule4Deductions.Add(savedRule4);
                        }
                        else
                        {
                            _context.Rule4Deductions.Add(rule4);
                            savedRule4Deductions.Add(rule4);
                        }
                    }

                    marketDetail.rule4Deductions = savedRule4Deductions;

                    _context.MarketDetails.Add(marketDetail);
                }
            }

            _context.SaveChanges();
        }

        private void DeleteContent()
        {
            _context.Database.ExecuteSqlRaw
                ("DELETE FROM [RunnerDetails];" +
                "DELETE FROM [Rule4Deductions];" +
                "DELETE FROM [MarketDetails];");
        }
    }
}
