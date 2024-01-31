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
            try
            {
                var eventTypes = _exchangeClient?.ListEventTypes(marketFilter) ??
                    throw new NullReferenceException($"Event Types null.");

                return eventTypes;
            }
            catch (NullReferenceException nullException)
            {
                Console.WriteLine(nullException.Message);
                throw;
            }
            catch (APINGException apiException)
            {
                Console.WriteLine(apiException.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public IList<EventResult> ListEvents(
            string eventTypeId = "7", TimeRange? timeRange = null)
        {
            var time = new TimeRange();

            if (timeRange == null)
            {
                time = BetFairQueryExtensions.RacingQueryTimeRange();
            }
            else
            {
                time = timeRange;
            }

            var marketFilter = new MarketFilter();
            marketFilter.EventTypeIds = new List<string> { eventTypeId }.ToHashSet();
            marketFilter.MarketStartTime = time;
            marketFilter.MarketCountries = eventTypeId.CountryCodes();

            try
            {
                var events = _exchangeClient?.ListEvents(marketFilter) ??
                    throw new NullReferenceException($"Events null.");

                return events;
            }
            catch (NullReferenceException nullException)
            {
                Console.WriteLine(nullException.Message);
                throw;
            }
            catch (APINGException apiException)
            {
                Console.WriteLine(apiException.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
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
                time = BetFairQueryExtensions.RacingQueryTimeRange();
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

            try
            {
                var marketCatalogues = _exchangeClient!.ListMarketCatalogue(
                    marketFilter, marketProjections, marketSort, maxResults);

                return marketCatalogues;
            }
            catch (NullReferenceException nullException)
            {
                Console.WriteLine(nullException.Message);
                throw;
            }
            catch (APINGException apiException)
            {
                Console.WriteLine(apiException.ToString());
                throw;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
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
                try
                {
                    marketBooks.AddRange(_exchangeClient!.ListMarketBook(
                         batch, priceProjection));
                }
                catch (NullReferenceException nullException)
                {
                    Console.WriteLine(nullException.Message);
                    continue;
                }
                catch (APINGException apiException)
                {
                    Console.WriteLine(apiException.ToString());
                    continue;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    continue;
                }
            }

            return marketBooks;
        }
    }
}