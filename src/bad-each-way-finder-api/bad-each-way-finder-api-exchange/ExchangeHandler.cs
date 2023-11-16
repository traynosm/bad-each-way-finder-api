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

        public IList<MarketCatalogue> ListMarketCatalogues(
    string eventTypeId = "7", TimeRange? timeRange = null, IEnumerable<string>? eventIds = null)
        {
            var marketFilter = new MarketFilter();
            marketFilter.EventTypeIds = new List<string> { eventTypeId }.ToHashSet();

            if (eventIds != null)
            {
                marketFilter.EventIds = eventIds.ToHashSet();
            }

            var time = new TimeRange();

            if (timeRange == null)
            {
                time.From = DateTime.Now;
                time.To = DateTime.Today.AddDays(2);
            }
            else
            {
                time = timeRange;
            }

            marketFilter.MarketStartTime = time;
            marketFilter.MarketCountries = eventTypeId.CountryCodes();
            marketFilter.MarketTypeCodes = eventTypeId.MarketTypes();
            var marketSort = MarketSort.FIRST_TO_START;
            var maxResults = "1000";

            //as an example we requested runner metadata 
            ISet<MarketProjection> marketProjections = new HashSet<MarketProjection>();
            marketProjections.Add(MarketProjection.EVENT);
            marketProjections.Add(MarketProjection.MARKET_DESCRIPTION);
            marketProjections.Add(MarketProjection.COMPETITION);
            marketProjections.Add(MarketProjection.EVENT_TYPE);
            marketProjections.Add(MarketProjection.RUNNER_DESCRIPTION);

            var marketCatalogues = _exchangeClient!.ListMarketCatalogue(
                marketFilter, marketProjections, marketSort, maxResults);

            return marketCatalogues;
        }

        public IList<MarketBook> ListMarketBooks(IList<string> marketIds)
        {
            //as an example we requested runner metadata 
            PriceProjection priceProjection = new PriceProjection()
            {
                PriceData = new HashSet<PriceData>
                {
                    PriceData.EX_BEST_OFFERS,
                    PriceData.EX_TRADED
                },
            };

            var marketBooks = new List<MarketBook>();

            foreach(var batch in marketIds.Chunk(10))
            {
                marketBooks.AddRange(_exchangeClient!.ListMarketBook(
                     batch, priceProjection));
            }

            return marketBooks;
        }
    }
}