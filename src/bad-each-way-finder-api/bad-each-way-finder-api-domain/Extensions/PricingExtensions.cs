using bad_each_way_finder_api_domain.Sportsbook;

namespace bad_each_way_finder_api_domain.Extensions
{
    public static class PricingExtensions
    {
        public static double ExpectedValue(this double sportsbookPrice, double exchangePrice)
        {
            return ((sportsbookPrice - 1) * (1 / exchangePrice)) - ((1 - (1 / exchangePrice)));
        }

        public static double PlacePart(this double sportsbookPrice, int denominator)
        {
            return denominator == 0 ? ((sportsbookPrice - 1) / 1) + 1 : ((sportsbookPrice - 1) / denominator) + 1;
        }

        public static string OddsString(this decimal[] priceParts)
        {
            return $"{priceParts[0]}/{priceParts[1]}";
        }

        public static string OddsString(this double[] priceParts)
        {
            return $"{priceParts[0]}/{priceParts[1]}";
        }

        public static string OddsString(this int[] priceParts)
        {
            return $"{priceParts[0]}/{priceParts[1]}";
        }

        public static double WinOverround(this MarketDetail marketDetail)
        {
            return marketDetail.runnerDetails
                .Where(r => r.winRunnerOdds != null && r.winRunnerOdds.@decimal > 0 && r.runnerStatus == "ACTIVE")
                .Sum(r => 1 / r.winRunnerOdds.@decimal) * 100;
        }

        public static double PlaceOverround(this MarketDetail marketDetail)
        {
            return marketDetail.runnerDetails
                .Where(r => r.winRunnerOdds != null && r.winRunnerOdds.@decimal > 0 && r.runnerStatus == "ACTIVE")
                .Sum(r => (1 / (((r.winRunnerOdds.@decimal - 1) / marketDetail.placeFractionDenominator) + 1))) * 100;
        }

    }
}
