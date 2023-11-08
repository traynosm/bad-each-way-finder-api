using bad_each_way_finder_api_domain.Exchange;

namespace bad_each_way_finder_api_exchange.Interfaces
{
    public interface IExchangeClient
    {
        IList<EventTypeResult> ListEventTypes(MarketFilter marketFilter, string locale = null);
        IList<EventResult> ListEvents(MarketFilter marketFilter, string locale = null);
    }
}
