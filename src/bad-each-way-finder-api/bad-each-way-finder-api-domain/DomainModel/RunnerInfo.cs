using bad_each_way_finder_api_domain.Extensions;
using bad_each_way_finder_api_domain.Sportsbook;

namespace bad_each_way_finder_api_domain.DomainModel
{
    public class RunnerInfo
    {
        //exchange properties
        public long RunnerSelectionId { get; set; }
        public string RunnerName { get; set; }
        public int RunnerOrder { get; set; }
        public double ExchangeWinPrice { get; set; }
        public double ExchangeWinSize { get; set; }
        public double ExchangePlacePrice { get; set; }
        public double ExchangePlaceSize { get; set; }

        //sportbook properties
        public double WinRunnerOddsDecimal { get; set; }
        public double EachWayPlacePart { get; set; }
        public double WinExpectedValue { get; set; }
        public double PlaceExpectedValue { get; set; }
        public double EachWayExpectedValue { get; set; }

        public RunnerInfo() { }

        public RunnerInfo(RunnerInfo runner, Race race)
        {
            RunnerSelectionId = runner.RunnerSelectionId;
            RunnerName = runner.RunnerName;
            RunnerOrder = runner.RunnerOrder;
            ExchangeWinPrice = runner.ExchangeWinPrice;
            ExchangeWinSize = runner.ExchangeWinSize;
            ExchangePlacePrice = runner.ExchangePlacePrice;
            ExchangePlaceSize = runner.ExchangePlaceSize;
            WinRunnerOddsDecimal = runner.WinRunnerOddsDecimal;
            EachWayPlacePart = runner.WinRunnerOddsDecimal.EachWayPlacePart(race.SportsbookPlaceFractionDenominator);
            WinExpectedValue = runner.WinExpectedValue;
            PlaceExpectedValue = runner.PlaceExpectedValue;
            EachWayExpectedValue = runner.EachWayExpectedValue;
        }
    }
}
