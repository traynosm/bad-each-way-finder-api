using bad_each_way_finder_api_auth.Interfaces;
using bad_each_way_finder_api_domain.Enums;
using bad_each_way_finder_api_domain.Exchange;
using bad_each_way_finder_api_exchange.Client;
using bad_each_way_finder_api_exchange.Interfaces;
using bad_each_way_finder_api_exchange.Settings;
using bad_each_way_finder_api_domain.Extensions;
using Microsoft.Extensions.Options;

namespace bad_each_way_finder_api_exchange
{
    public class ExchangeHandler : IExchangeHandler
    {
        private readonly IExchangeClient _exchangeClient;
        private readonly IAuthHandler _authHandler;
        private readonly IOptions<ExchangeSettings> _options;

        private const Bookmaker _bookmaker = Bookmaker.BetfairExchange;

        public ExchangeHandler(IExchangeClient exchangeClient, IAuthHandler authHandler, 
            IOptions<ExchangeSettings> options)
        {
            _exchangeClient = exchangeClient;
            _authHandler = authHandler;
            _options = options;

            if (_authHandler.TryLogin(_bookmaker))
            {
                _exchangeClient = new ExchangeClient(
                    _options.Value.Url,
                    _authHandler.AppKey,
                    _authHandler.SessionTokens[_bookmaker]);
            }
        }

        public bool TryLogin() =>
            _authHandler.TryLogin(_bookmaker);

        public bool Login(string username = "", string password = "") =>
            _authHandler.Login(username, password, _bookmaker);

        public bool SessionValid() =>
            _authHandler.SessionValid(_bookmaker);

        public IList<EventTypeResult> ListEventTypes()
        {
            var marketFilter = new MarketFilter();

            var eventTypes = _exchangeClient?.ListEventTypes(marketFilter) ??
                throw new NullReferenceException($"Event Types null.");

            return eventTypes;
        }
        public IList<EventResult> ListEvents(
            string eventTypeId = "7", TimeRange? timeRange = null)
        {
            var time = new TimeRange();

            if (timeRange == null)
            {
                time = new TimeRange()
                {
                    From = DateTime.Now,
                    To = DateTime.Today.AddDays(1)
                };
            }
            else
            {
                time = timeRange;
            }

            var marketFilter = new MarketFilter();
            marketFilter.EventTypeIds = new List<string> { eventTypeId }.ToHashSet();
            marketFilter.MarketStartTime = time;
            marketFilter.MarketCountries = eventTypeId.CountryCodes();

            var events = _exchangeClient?.ListEvents(marketFilter) ??
                throw new NullReferenceException($"Events null.");

            return events;
        }





        //Expose methods that
    }
}