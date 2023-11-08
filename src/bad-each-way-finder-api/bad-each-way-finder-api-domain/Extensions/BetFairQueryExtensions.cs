﻿using bad_each_way_finder_api_domain.Enums;

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
    }

}