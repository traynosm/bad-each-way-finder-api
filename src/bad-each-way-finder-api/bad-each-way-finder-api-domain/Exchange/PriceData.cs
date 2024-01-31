using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace bad_each_way_finder_api_domain.Exchange
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum PriceData
    {
        SP_AVAILABLE, SP_TRADED,
        EX_BEST_OFFERS, EX_ALL_OFFERS, EX_TRADED,
    }
}
