using Newtonsoft.Json;
using System.Text;

namespace bad_each_way_finder_api_domain.Exchange
{
    public class EventType
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        public override string ToString()
        {
            return new StringBuilder().AppendFormat("{0}", "EventType")
                        .AppendFormat(" : Id={0}", Id)
                        .AppendFormat(" : Name={0}", Name)
                        .ToString();
        }
    }
}
