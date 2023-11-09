using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace bad_each_way_finder_api_domain.Exchange
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum MatchProjection
    {
        NO_ROLLUP, ROLLED_UP_BY_PRICE, ROLLED_UP_BY_AVG_PRICE
    }
}
