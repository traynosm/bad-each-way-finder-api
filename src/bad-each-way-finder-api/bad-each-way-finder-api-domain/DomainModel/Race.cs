﻿namespace bad_each_way_finder_api_domain.DomainModel
{
    public class Race
    {
        public string EventId { get; set; }
        public string EventName { get; set; }
        public DateTime EventDateTime { get; set; }
        public string ExchangeWinMarketId { get; set; }
        public string ExchangePlaceMarketId { get; set; }

        //sportbook properties
        public string SportsbookWinMarketId { get; set; }
        public bool SportsbookEachwayAvailable { get; set; }
        public int SportsbookNumberOfPlaces { get; set; }
        public int SportsbookPlaceFractionDenominator { get; set; }
        public List<RunnerInfo> Runners { get; set; }
        public double WinOverRound { get; set; }
        public double PlaceOverRound { get; set; }
    }
}
