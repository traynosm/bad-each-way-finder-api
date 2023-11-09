using Newtonsoft.Json;
using Newtonsoft.Json.Converters;


namespace bad_each_way_finder_api_domain.Exchange
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum OrderProjection
    {
        ALL, EXECUTABLE, EXECUTION_COMPLETE
    }
}
