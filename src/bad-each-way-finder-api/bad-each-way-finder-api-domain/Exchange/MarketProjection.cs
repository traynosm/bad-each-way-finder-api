using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace bad_each_way_finder_api_domain.Exchange
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MarketProjection
    {
        COMPETITION, EVENT, EVENT_TYPE, MARKET_DESCRIPTION, RUNNER_DESCRIPTION, RUNNER_METADATA
    }
}
