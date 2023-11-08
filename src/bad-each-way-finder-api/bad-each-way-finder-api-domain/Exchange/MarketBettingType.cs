using Newtonsoft.Json.Converters;
using System.Text.Json.Serialization;

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
