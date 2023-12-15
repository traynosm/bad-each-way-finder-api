using bad_each_way_finder_api.Areas.Identity.Data;
using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.Exchange;
using Microsoft.EntityFrameworkCore;

namespace bad_each_way_finder_api.Repository
{
    public class ExchangeDatabaseService : DatabaseService, IExchangeDatabaseService
    {
        public ExchangeDatabaseService(BadEachWayFinderApiContext context, ILogger<DatabaseService> logger) : 
            base(context, logger)
        {

        }

        public void AddOrUpdateMarketCatalogues(IEnumerable<MarketCatalogue> marketCatalogues, bool clearData = true) 
        {
            //if (clearData && _context.MarketCatalogues.Any(m => m.Event!.OpenDate!.Value.Date == DateTime.Today))
            //{
            //    DeleteContent();
            //}

            foreach (var  marketCatalogue in marketCatalogues)
            {
                var savedMarketCatalogue = _context.MarketCatalogues.Find(marketCatalogue.MarketId);

                if(savedMarketCatalogue != null)
                {
                    _context.Entry(savedMarketCatalogue).CurrentValues.SetValues(marketCatalogue);
                }
                else
                {
                    var savedEvent = _context.Events.Find(marketCatalogue.Event!.Id);

                    if (savedEvent != null)
                    {
                        marketCatalogue.Event = savedEvent;
                    }
                    else
                    {
                        _context.Events.Add(marketCatalogue.Event);
                    }

                    var savedEventType = _context.EventTypes.Find(marketCatalogue.EventType!.Id);

                    if (savedEventType != null)
                    {
                        marketCatalogue.EventType = savedEventType;
                    }
                    else
                    {
                        _context.EventTypes.Add(marketCatalogue.EventType);
                    }   

                    var savedRunners = new List<RunnerDescription>();
                    foreach(var runner in marketCatalogue.Runners)
                    {
                        var savedRunner = _context.RunnerDescriptions.Find(runner.SelectionId);

                        if (savedRunner != null)
                        {
                            savedRunners.Add(savedRunner);
                        }
                        else
                        {
                            _context.RunnerDescriptions.Add(runner);
                            savedRunners.Add(runner);
                        }
                    }

                    marketCatalogue.Runners = savedRunners;

                    _context.MarketCatalogues.Add(marketCatalogue);
                }
            }

            _context.SaveChanges();
        }

        public void AddOrUpdateMarketBooks(IEnumerable<MarketBook> marketBooks)
        {
            foreach(var marketBook in marketBooks) 
            {
                var savedMarketBook = _context.MarketCatalogues.Find(marketBook.MarketId);

                if(savedMarketBook != null)
                {
                    _context.Entry(savedMarketBook).CurrentValues.SetValues(marketBook);
                }
                else
                {
                    var savedRunners = new List<Runner>();
                    foreach (var runner in marketBook.Runners)
                    {
                        var savedRunner = _context.Runners.Find(runner.SelectionId);

                        if (savedRunner != null)
                        {
                            _context.Entry(savedRunner).CurrentValues.SetValues(runner);
                            savedRunners.Add(savedRunner);
                        }
                        else
                        {
                            _context.Runners.Add(runner);
                            savedRunners.Add(runner);
                        }
                    }
                    marketBook.Runners = savedRunners;

                    _context.MarketBooks.Add(marketBook);
                }
            }
            _context.SaveChanges();
        }

       private void DeleteContent()
       {
            _context.Database.ExecuteSqlRaw
                ("DELETE FROM [RunnerDescriptions];" +
                "DELETE FROM [Competitions];" +
                "DELETE FROM [MarketCatalogues];" +
                "DELETE FROM [MarketDescriptions];" +
                "DELETE FROM [ExchangeEvents];" +
                "DELETE FROM [EventTypes];");
       }
    }
}
