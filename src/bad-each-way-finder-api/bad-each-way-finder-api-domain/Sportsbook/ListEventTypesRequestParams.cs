using Newtonsoft.Json;

namespace bad_each_way_finder_api_domain.Sportsbook
{
    public class ListEventTypesRequestParams
    {
        [JsonProperty(PropertyName = "marketFilter")]
        public SportsbookMarketFilter MarketFilter { get; set; }
    }
}
