using bad_each_way_finder_api_domain.Enums;
using bad_each_way_finder_api_domain.Exchange;

namespace bad_each_way_finder_api_domain.Extensions
{
    public static class BetFairQueryExtensions
    {
        public static string SportMap(this Sport sport)
        {
            switch (sport)
            {
                case Sport.Racing:
                    return "7";
            }

            return "0";
        }
        public static HashSet<string> CountryCodes(this Sport sport)
        {
            return CountryCodes(sport.SportMap());
        }
        public static HashSet<string> CountryCodes(this string eventTypeId)
        {
            switch (eventTypeId)
            {
                case "7":
                    return new HashSet<string>()
                    {
                        "GB",
                        "IE"
                    };
            }

            return new HashSet<string>();
        }
        public static HashSet<string> MarketTypes(this Sport sport)
        {
            return MarketTypes(sport.SportMap());
        }

        public static HashSet<string> MarketTypes(this string eventTypeId)
        {
            switch (eventTypeId)
            {
                case "7":
                    return new HashSet<string>()
                    {
                        "WIN",
                        "PLACE",
                        "OTHER_PLACE"
                    };
            }

            return new HashSet<string>();
        }

        public static TimeRange RacingQueryTimeRange()
        {
            if(DateTime.Now.Hour < 17)
            {
                return new TimeRange()
                {
                    From = DateTime.Today,
                    To = DateTime.Today.AddDays(1)
                };
            }
            return new TimeRange()
            {
                From = DateTime.Today,
                To = DateTime.Today.AddDays(2)
            }; ;
        }
    }
}
