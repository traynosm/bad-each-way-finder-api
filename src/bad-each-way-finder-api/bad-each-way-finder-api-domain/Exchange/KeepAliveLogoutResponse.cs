using System.Runtime.Serialization;

namespace bad_each_way_finder_api_domain.Exchange
{
    [DataContract]
    public class KeepAliveLogoutResponse
    {
        [DataMember(Name = "token")]
        public string Token { get; set; }
        [DataMember(Name = "product")]
        public string Product { get; set; }
        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "error")]
        public string Error { get; set; }
    }
}
