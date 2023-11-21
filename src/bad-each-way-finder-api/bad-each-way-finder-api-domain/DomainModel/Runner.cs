using bad_each_way_finder_api_domain.Sportsbook;

namespace bad_each_way_finder_api_domain.DomainModel
{
    public class Runner
    {
        //exchange properties
        public long RunnerSelectionId { get; set; }
        public string RunnerName { get; set; }
        public double ExchangeWinPrice{ get; set; }
        public double ExchangePlacePrice { get; set; }

        //sportbook properties
        public WinRunnerOdds WinRunnerOdds { get; set; }
        public double WinExpectedValue { get; set; }
        public double PlaceExpectedValue { get; set; }
        public double EachWayExpectedValue { get; set; }
    }
}
