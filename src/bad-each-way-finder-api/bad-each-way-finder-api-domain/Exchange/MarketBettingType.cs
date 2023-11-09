using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace bad_each_way_finder_api_domain.Exchange
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MarketBettingType
    {
        ODDS,
        LINE,
        RANGE,
        ASIAN_HANDICAP_DOUBLE_LINE,
        ASIAN_HANDICAP_SINGLE_LINE,
        FIXED_ODDS
    }
}
