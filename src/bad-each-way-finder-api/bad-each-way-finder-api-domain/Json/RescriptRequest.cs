using Newtonsoft.Json;

namespace bad_each_way_finder_api_exchange.Json
{
    [JsonObject(MemberSerialization.OptIn)]
    public class RescriptRequest
    {

        [JsonProperty(PropertyName = "")]
        public IDictionary<string, object> args { get; set; }

        public RescriptRequest(IDictionary<string, object> args)
        {
            this.args = args;
        }
    }
}
