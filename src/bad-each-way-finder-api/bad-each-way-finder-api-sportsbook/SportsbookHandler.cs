﻿using bad_each_way_finder_api_auth.Interfaces;
using bad_each_way_finder_api_domain.Enums;
using bad_each_way_finder_api_domain.Exchange;
using bad_each_way_finder_api_domain.Extensions;
using bad_each_way_finder_api_domain.Sportsbook;
using bad_each_way_finder_api_sportsbook.Client;
using bad_each_way_finder_api_sportsbook.Interfaces;
using bad_each_way_finder_api_sportsbook.Settings;
using Microsoft.Extensions.Options;

namespace bad_each_way_finder_api_sportsbook
{
    public class SportsbookHandler : ISportsbookHandler
    {
        private readonly ISportsbookClient _client;
        private readonly IOptions<SportsbookSettings> _options;
        private readonly IAuthHandler _authHandler;


        public SportsbookHandler(ISportsbookClient client, IOptions<SportsbookSettings> options,
            IAuthHandler authHandler)
        {
            _client = client;
            _options = options;
            _authHandler = authHandler;

            if (TryLogin())
            {
                _client = new SportsbookClient(
                _options.Value.Url,
                    _authHandler.AppKey,
                    _authHandler.SessionTokens[Bookmaker.BetfairSportsbook]);
            }
        }

        public bool TryLogin() =>
            _authHandler.TryLogin(Bookmaker.BetfairSportsbook);

        public bool Login(string username = "", string password = "") =>
            _authHandler.Login(username, password, Bookmaker.BetfairSportsbook);

        public bool SessionValid() =>
            _authHandler.SessionValid(Bookmaker.BetfairSportsbook);

        public IEnumerable<EventTypeResult> ListEventTypes()
        {
            var marketFilter = new MarketFilter();

            var eventTypes = _client?
                    .ListEventTypes(marketFilter) ??
                throw new NullReferenceException($"Event Types null.");

            return eventTypes;
        }

        public IEnumerable<Event> ListEventsByEventType(
            string eventTypeId = "7", TimeRange? timeRange = null)
        {
            var time = new TimeRange();

            if (timeRange == null)
            {
                switch (eventTypeId)
                {
                    case "7":
                        time = new TimeRange()
                        {
                            //From = DateTime.Today,
                            From = DateTime.Today,
                            To = DateTime.Today.AddDays(1)
                        };
                        break;
                }
            }
            else
            {
                time = timeRange;
            }

            var events = _client?
                    .ListEventsByEventType(eventTypeId, time) ??
                throw new NullReferenceException($"Events null.");

            return events.Select(e => e.Event)
                .Where(ev => eventTypeId.CountryCodes()
                    .Contains(ev.CountryCode));
        }

        public IEnumerable<MarketCatalogue> ListMarketCatalogues(
        ISet<string> eventIds, string eventTypeId = "7")
        {
            var marketTypes = new List<string>();

            switch (eventTypeId)
            {
                case "7":
                    marketTypes = new List<string>() { "WIN" };
                    break;
            }
            var marketCatalogues = _client?
                .ListMarketCatalogue(new SportsbookMarketFilter()
                {
                    EventIds = eventIds,
                    MarketTypes = marketTypes.ToArray(),
                },
            maxResults: "1000")
                ??
                throw new NullReferenceException($"Market Catalogues null.");

            return marketCatalogues;
        }

        public MarketDetails ListPrices(IEnumerable<string> marketIds)
        {
            var prices = _client?
                    .ListMarketPrices(marketIds) ??
                throw new NullReferenceException($"Prices null.");

            return prices;
        }



    }
}
