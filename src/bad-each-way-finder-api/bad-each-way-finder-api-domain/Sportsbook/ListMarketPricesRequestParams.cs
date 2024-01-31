using Newtonsoft.Json;

namespace bad_each_way_finder_api_domain.Sportsbook
{
    public class ListMarketPricesRequestParams
    {
        [JsonProperty(PropertyName = "marketIds")]
        public IList<string> MarketIds { get; set; }
    }
}
