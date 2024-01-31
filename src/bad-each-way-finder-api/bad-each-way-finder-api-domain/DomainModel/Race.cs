using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bad_each_way_finder_api_domain.DomainModel
{
    public class Race
    {   
        //sportsbook props
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string SportsbookWinMarketId { get; set; }
        public bool SportsbookEachwayAvailable { get; set; }
        public int SportsbookNumberOfPlaces { get; set; }
        public int SportsbookPlaceFractionDenominator { get; set; }
        public virtual List<RunnerInfo> Runners { get; set; }
        public double WinOverRound { get; set; }
        public double PlaceOverRound { get; set; }

        public string EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDateTime { get; set; }
        public DateTime LastUpdated { get; set; }

        //exchange props
        public string ExchangeWinMarketId { get; set; }
        public string ExchangePlaceMarketId { get; set; }
    }
}
