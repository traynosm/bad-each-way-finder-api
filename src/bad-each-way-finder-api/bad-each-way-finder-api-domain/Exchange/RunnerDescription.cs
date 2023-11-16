using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace bad_each_way_finder_api_domain.Exchange
{
    public class RunnerDescription
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [JsonProperty(PropertyName = "selectionId")]
        public long SelectionId { get; set; }

        [JsonProperty(PropertyName = "runnerName")]
        public string RunnerName { get; set; }

        [JsonProperty(PropertyName = "handicap")]
        public double Handicap { get; set; }

        [JsonProperty(PropertyName = "sortPriority")]
        public int SortPriority { get; set; }

        [NotMapped]
        [JsonProperty(PropertyName = "metadata")]
        public Dictionary<string, string> Metadata { get; set; }

        public override string ToString()
        {
            return new StringBuilder().AppendFormat("{0}", "RunnerDescription")
                        .AppendFormat(" : SelectionId={0}", SelectionId)
                        .AppendFormat(" : runnerName={0}", RunnerName)
                        .AppendFormat(" : Handicap={0}", Handicap)
                        .AppendFormat(" : SortPriority={0}", SortPriority)
                        .AppendFormat(" : Metadata={0}", Metadata)
                        .ToString();
        }
    }

}
