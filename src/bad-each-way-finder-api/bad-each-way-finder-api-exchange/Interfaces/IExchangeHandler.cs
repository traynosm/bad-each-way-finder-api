using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.Exchange;

namespace bad_each_way_finder_api_exchange.Interfaces
{
    public interface IExchangeHandler : IBetfairHandler
    {
        IList<EventTypeResult> ListEventTypes();
    }
}
