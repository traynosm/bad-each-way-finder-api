using Newtonsoft.Json;

namespace bad_each_way_finder_api_domain.Sportsbook
{
    public class ListMarketCatalogueRequestParams
    {
        [JsonProperty(PropertyName = "marketFilter")]
        public SportsbookMarketFilter MarketFilter { get; set; }

        [JsonProperty(PropertyName = "maxResults")]
        public string MaxResults { get; set; }
    }
}
