using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.Exchange;
using bad_each_way_finder_api_domain.Sportsbook;

namespace bad_each_way_finder_api_sportsbook.Interfaces
{
    public interface ISportsbookHandler : IBetfairHandler
    {
        IEnumerable<EventTypeResult> ListEventTypes();
        IEnumerable<Event> ListEventsByEventType(
            string eventTypeId = "7", TimeRange? timeRange = null);
        IEnumerable<MarketCatalogue> ListMarketCatalogues(
            ISet<string> eventIds, string eventTypeId = "7");
        MarketDetails ListPrices(IEnumerable<string> marketIds);

    }
}
