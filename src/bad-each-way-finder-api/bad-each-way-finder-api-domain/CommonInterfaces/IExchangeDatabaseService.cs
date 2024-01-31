using bad_each_way_finder_api_domain.Exchange;

namespace bad_each_way_finder_api_domain.CommonInterfaces
{
    public interface IExchangeDatabaseService
    {
        void AddOrUpdateMarketCatalogues(IEnumerable<MarketCatalogue> marketCatalogues, bool clearData = true);
        void AddOrUpdateMarketBooks(IEnumerable<MarketBook> marketBooks);
    }
}
