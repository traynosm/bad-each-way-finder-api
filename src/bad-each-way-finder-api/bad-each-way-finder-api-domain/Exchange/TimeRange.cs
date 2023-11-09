﻿using Newtonsoft.Json;
using System.Text;

namespace bad_each_way_finder_api_domain.Exchange
{
    public class TimeRange
    {
        [JsonProperty(PropertyName = "from")]
        public DateTime From { get; set; }

        [JsonProperty(PropertyName = "to")]
        public DateTime To { get; set; }

        public override string ToString()
        {
            return new StringBuilder().AppendFormat("{0}", "TimeRange")
                        .AppendFormat(" : From={0}", From)
                        .AppendFormat(" : To={0}", To)
                        .ToString();
        }
    }
}
