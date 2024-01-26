using bad_each_way_finder_api_domain.Exchange;
using bad_each_way_finder_api_domain.Sportsbook;

namespace bad_each_way_finder_api_sportsbook.Interfaces
{
    public interface ISportsbookClient
    {
        IEnumerable<EventTypeResult> ListEventTypes(
            MarketFilter marketFilter, string? locale = null);
        IEnumerable<EventResult> ListEventsByEventType(string eventTypeId, TimeRange timeRange);
        IEnumerable<MarketCatalogue> ListMarketCatalogue(
            SportsbookMarketFilter marketFilter, string maxResults = "1", string? locale = null);
        MarketDetails ListMarketPrices(IEnumerable<string> marketIds);
    }
}
