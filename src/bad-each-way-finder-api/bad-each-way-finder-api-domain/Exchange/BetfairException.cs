using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace bad_each_way_finder_api_domain.Exchange
{
    public class BetfairException
    {
        // exception in json-rpc format
        [JsonProperty(PropertyName = "data")]
        public JObject Data { get; set; }		// actual exception details


        // exception in rescript format
        [JsonProperty(PropertyName = "detail")]
        public JObject Detail { get; set; }		// actual exception details

    }
}
