using bad_each_way_finder_api.Migrations;
using bad_each_way_finder_api.Repository;
using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.Exchange;
using bad_each_way_finder_api_domain.Sportsbook;
using bad_each_way_finder_api_exchange.Interfaces;
using bad_each_way_finder_api_sportsbook.Interfaces;

namespace bad_each_way_finder_api.Services
{
    public class PropositionService : IPropositionService
    {
        private readonly IExchangeHandler _exchangeHandler;
        private readonly ISportsbookHandler _sportsbookHandler;

        public PropositionService(IExchangeHandler exchangeHandler, ISportsbookHandler sportsbookHandler)
        {
            _exchangeHandler = exchangeHandler;
            _sportsbookHandler = sportsbookHandler;
        }

        public void GettingStuff()
        {
            try
            {
                var marketDetails = GetMarketDetails();

                var linkedWinMarketIds = marketDetails.marketDetails.Select(l => l.linkedMarketId);

                var winMarketBooks = GetMarketBooks(linkedWinMarketIds.ToList());

            }
            catch (Exception ex)
            {

            }
        }

        private MarketDetails GetMarketDetails() 
        {
            var loginSuccess = _sportsbookHandler.TryLogin();

            if (loginSuccess)
            {
                var eventIds = _sportsbookHandler.ListEventsByEventType("7");
                var eId = _sportsbookHandler.ListMarketCatalogues(eventIds.Select(
                    e => e.Id).ToHashSet());
                var result = _sportsbookHandler.ListPrices(eId.Select(
                    e => e.MarketId).ToList());
                return result;
            }
            else
            {
                throw new Exception("Login failed");
            }
        }

        private IList<MarketBook> GetMarketBooks(List<string> marketIds)
        {
            var loginSuccess = _exchangeHandler.TryLogin();

            if (loginSuccess)
            {
                var result = _exchangeHandler.ListMarketBooks(marketIds);

                return result;
            }
            else
            {
                throw new Exception("Login failed");
            }
        }
    }
}
