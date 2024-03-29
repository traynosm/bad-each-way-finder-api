﻿using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.Exchange;

namespace bad_each_way_finder_api_exchange.Interfaces
{
    public interface IExchangeHandler : IBetfairHandler
    {
        IList<EventTypeResult> ListEventTypes();
        IList<EventResult> ListEvents(
            string eventTypeId = "7", TimeRange? timeRange = null);
        IList<MarketCatalogue> ListMarketCatalogues(
            string eventTypeId = "7", TimeRange? timeRange = null, 
            IEnumerable<string>? eventIds = null);
        IList<MarketBook> ListMarketBooks(IList<string> marketIds);
    }
}
