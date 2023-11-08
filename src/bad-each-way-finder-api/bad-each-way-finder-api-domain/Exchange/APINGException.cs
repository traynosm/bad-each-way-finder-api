using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace bad_each_way_finder_api_domain.Exchange
{
    public class APINGException : Exception
    {
        public APINGException(SerializationInfo info, StreamingContext context)
        {
            ErrorDetails = info.GetString("errorDetails");
            ErrorCode = info.GetString("errorCode");
            RequestUUID = info.GetString("requestUUID");
        }

        public APINGException()
        {

        }

        [JsonProperty(PropertyName = "errorDetails")]
        public string ErrorDetails { get; set; }

        [JsonProperty(PropertyName = "errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty(PropertyName = "requestUUID")]
        public string RequestUUID { get; set; }
    }
}
