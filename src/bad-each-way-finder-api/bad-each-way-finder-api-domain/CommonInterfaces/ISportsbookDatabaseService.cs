using bad_each_way_finder_api_domain.Sportsbook;

namespace bad_each_way_finder_api_domain.CommonInterfaces
{
    public interface ISportsbookDatabaseService
    {
        void AddOrUpdateMarketDetails(MarketDetails marketDetails, bool clearData = true);
    }
}
