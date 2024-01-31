using bad_each_way_finder_api_domain.Exchange;
using Newtonsoft.Json;

namespace bad_each_way_finder_api_domain.Sportsbook
{
    public class SportsbookMarketFilter : MarketFilter
    {
        [JsonProperty(PropertyName = "timeRange")]
        public TimeRange TimeRange { get; set; }

        [JsonProperty(PropertyName = "marketTypes")]
        public string[] MarketTypes { get; set; }
    }
}
