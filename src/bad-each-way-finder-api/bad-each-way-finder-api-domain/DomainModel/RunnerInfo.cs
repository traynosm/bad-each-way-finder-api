using bad_each_way_finder_api_domain.Exchange;
using bad_each_way_finder_api_domain.Extensions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bad_each_way_finder_api_domain.DomainModel
{
    public class RunnerInfo
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public string Id { get; set; }
        public long RunnerSelectionId { get; set; }
        public string RunnerName { get; set; }
        public int RunnerOrder { get; set; }

        //exchange properties
        public double ExchangeWinPrice { get; set; }
        public double ExchangeWinSize { get; set; }
        public double ExchangePlacePrice { get; set; }
        public double ExchangePlaceSize { get; set; }

        //sportbook properties
        public double WinRunnerOddsDecimal { get; set; }
        public int WinRunnerOddsNumerator { get; set; }
        public int WinRunnerOddsDenominator { get; set; }
        public double EachWayPlacePart { get; set; }
        public double WinExpectedValue { get; set; }
        public double PlaceExpectedValue { get; set; }
        public double EachWayExpectedValue { get; set; }
        public string RunnerStatus { get; set; }


        public RunnerInfo() { }

        public RunnerInfo(RunnerInfo runner, Race race)
        {
            Id = $"{race.EventId}{runner.RunnerSelectionId}"; //have to make this unique to the race
            RunnerSelectionId = runner.RunnerSelectionId;
            RunnerName = runner.RunnerName;
            RunnerOrder = runner.RunnerOrder;
            ExchangeWinPrice = runner.ExchangeWinPrice;
            ExchangeWinSize = runner.ExchangeWinSize;
            ExchangePlacePrice = runner.ExchangePlacePrice;
            ExchangePlaceSize = runner.ExchangePlaceSize;
            WinRunnerOddsDecimal = runner.WinRunnerOddsDecimal;
            WinRunnerOddsNumerator = runner.WinRunnerOddsNumerator;
            WinRunnerOddsDenominator = runner.WinRunnerOddsDenominator;
            EachWayPlacePart = runner.WinRunnerOddsDecimal.EachWayPlacePart(race.SportsbookPlaceFractionDenominator);
            WinExpectedValue = runner.WinExpectedValue;
            PlaceExpectedValue = runner.PlaceExpectedValue;
            EachWayExpectedValue = runner.EachWayExpectedValue;
            RunnerStatus = runner.RunnerStatus;
        }
    }
}
