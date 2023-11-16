using bad_each_way_finder_api_domain.Exchange;
using bad_each_way_finder_api_domain.Extensions;
using bad_each_way_finder_api_domain.Sportsbook;
using bad_each_way_finder_api_exchange.Json;
using bad_each_way_finder_api_sportsbook.Interfaces;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace bad_each_way_finder_api_sportsbook.Client
{
    public class SportsbookClient : HttpClient, ISportsbookClient
    {
        public string EndPoint { get; private set; }
        public const string APPKEY_HEADER = "X-Application";
        public const string SESSION_TOKEN_HEADER = "X-Authentication";
        public WebHeaderCollection CustomHeaders { get; set; }
        private static readonly string LIST_EVENT_TYPES_METHOD = "listEventTypes";
        private static readonly string LIST_EVENTS_METHOD = "listEvents";
        private static readonly string LIST_MARKET_TYPES_METHOD = "listMarketTypes";
        private static readonly string LIST_MARKET_CATALOGUE_METHOD = "listMarketCatalogue";
        private static readonly string LIST_MARKET_BOOK_METHOD = "listMarketBook";
        private static readonly string LIST_MARKET_PRICES_METHOD = "listMarketPrices";
        private static readonly string FILTER = "filter";
        private static readonly string LOCALE = "locale";
        private static readonly string MARKET_PROJECTION = "marketProjection";
        private static readonly string MATCH_PROJECTION = "matchProjection";
        private static readonly string ORDER_PROJECTION = "orderProjection";
        private static readonly string PRICE_PROJECTION = "priceProjection";
        private static readonly string SORT = "sort";
        private static readonly string MAX_RESULTS = "maxResults";
        private static readonly string MARKET_IDS = "marketIds";
        private static readonly string MARKET_ID = "marketId";
        private static readonly string EVENT_TYPE_IDS = "eventTypeIds";
        private static readonly string EVENT_IDS = "eventIds";
        private static readonly string RUNNER_IDS = "runnerIds";
        private static readonly string SIDE = "side";
        private static readonly string GROUP_BY = "groupBy";

        public SportsbookClient()
        {

        }

        public SportsbookClient(string endPoint, string appKey, string sessionToken)
        {
            EndPoint = endPoint + "/rest/v1.0/";

            base.BaseAddress = new Uri(EndPoint);

            base.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (appKey != null)
            {
                base.DefaultRequestHeaders.TryAddWithoutValidation(APPKEY_HEADER, appKey);
            }
            if (sessionToken != null)
            {
                base.DefaultRequestHeaders.TryAddWithoutValidation(SESSION_TOKEN_HEADER, sessionToken);
            }
        }

        private T Invoke<T>(string method, object obj)
        {
            if (method == null)
                throw new ArgumentNullException("method");
            if (method.Length == 0)
                throw new ArgumentException(null, "method");

            var body = JsonConvert.Serialize(obj);

            Console.WriteLine("Calling Sportsbook: " + method + " With body: " + body);

            HttpContent content = new StringContent(body, Encoding.UTF8, "application/json");

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            ServicePointManager.Expect100Continue = false;

            var response = base.PostAsync(method + "/", content).Result;

            var responseContent = response.Content.ReadAsStringAsync().Result;

            var result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseContent);

            return result;
        }
        public IEnumerable<EventTypeResult> ListEventTypes(MarketFilter marketFilter, string? locale = null)
        {
            var obj = new
            {
                listEventTypesRequestParams = new ListEventTypesRequestParams()
                {
                    MarketFilter = new SportsbookMarketFilter()
                }
            };

            return Invoke<List<EventTypeResult>>(LIST_EVENT_TYPES_METHOD, obj);
        }

        public IEnumerable<EventResult> ListEventsByEventType(string eventTypeId, TimeRange timeRange)
        {
            var obj = new
            {
                listEventsRequestParams = new ListEventsRequestParams()
                {
                    MarketFilter = new SportsbookMarketFilter()
                    {
                        EventTypeIds = new HashSet<string>() { eventTypeId },
                        TimeRange = timeRange,
                        MarketCountries = eventTypeId.CountryCodes()
                    }
                }
            };

            return Invoke<List<EventResult>>(LIST_EVENTS_METHOD, obj);
        }

        public IEnumerable<MarketCatalogue> ListMarketCatalogue(
            SportsbookMarketFilter marketFilter, string maxResults = "1", string? locale = null)
        {
            var obj = new
            {
                listMarketCatalogueRequestParams = new ListMarketCatalogueRequestParams()
                {
                    MarketFilter = marketFilter,
                    MaxResults = maxResults
                },
            };

            return Invoke<List<MarketCatalogue>>(LIST_MARKET_CATALOGUE_METHOD, obj);
        }

        public MarketDetails ListMarketPrices(IEnumerable<string> marketIds)
        {
            MarketDetails marketDetails = new MarketDetails() { marketDetails = new List<MarketDetail>() };

            foreach (var batch in marketIds.Chunk(100))
            {
                var obj = new
                {
                    listMarketPricesRequestParams = new ListMarketPricesRequestParams()
                    {
                        MarketIds = batch
                    },
                };

                var batchResult = Invoke<MarketDetails>(LIST_MARKET_PRICES_METHOD, obj);

                foreach (var md in batchResult.marketDetails)
                {
                    marketDetails.marketDetails.Add(md);
                }
            }

            return marketDetails;
        }
    }
}
