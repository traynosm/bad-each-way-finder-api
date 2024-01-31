using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bad_each_way_finder_api_domain.Sportsbook
{
    public class MarketDetail
    {
        [Key]
        public string marketId { get; set; }
        public string eventId { get; set; }
        public string marketName { get; set; }
        public string marketType { get; set; }
        public DateTime marketStartTime { get; set; }
        public bool inplay { get; set; }
        public bool livePriceAvailable { get; set; }
        public bool guaranteedPriceAvailable { get; set; }
        public List<RunnerDetail> runnerDetails { get; set; }
        public bool eachwayAvailable { get; set; }
        public int numberOfPlaces { get; set; }
        public int placeFractionNumerator { get; set; }
        public int placeFractionDenominator { get; set; }
        [NotMapped]
        public List<string> legTypes { get; set; }
        public string? linkedMarketId { get; set; }
        public List<Rule4Deduction> rule4Deductions { get; set; }
        public string? rampMarketId { get; set; }
        public string marketStatus { get; set; }
    }
}
