using System;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using bad_each_way_finder_api_auth.Interfaces;
using bad_each_way_finder_api_domain.Enums;
using bad_each_way_finder_api_domain.Exchange;

namespace bad_each_way_finder_api_auth.Clients
{

    public class AuthClient : IAuthClient
    {
        public AuthClient()
        {
        }

        private static HttpClientHandler GetWebRequestHandlerWithCert(string certFilename)
        {
            var clientHandler = new HttpClientHandler();
            return clientHandler;
        }

        private const string DEFAULT_COM_BASEURL_BETFAIR = "https://identitysso.betfair.com/";

        private static HttpClient InitHttpClientInstance(HttpClientHandler clientHandler, string appKey, Bookmaker bookmaker)
        {
            var baseUrl = DEFAULT_COM_BASEURL_BETFAIR;

            var client = new HttpClient(clientHandler);
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Add("X-Application", appKey);
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        private static FormUrlEncodedContent GetLoginBodyAsContent(string username, string password)
        {
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("username", username));
            postData.Add(new KeyValuePair<string, string>("password", password));
            return new FormUrlEncodedContent(new List<KeyValuePair<string, string>>());
        }

        public KeepAliveLogoutResponse Login(string username, string password, string appKey, Bookmaker bookmaker = Bookmaker.BetfairSportsbook)
        {
            var handler = GetWebRequestHandlerWithCert("");
            var client = InitHttpClientInstance(handler, appKey, bookmaker);
            var content = GetLoginBodyAsContent(username, password);
            var result = client.PostAsync($"api/login?username={username}&password={password}", content).Result;
            result.EnsureSuccessStatusCode();
            var jsonSerialiser = new DataContractJsonSerializer(typeof(KeepAliveLogoutResponse));
            var stream = new MemoryStream(result.Content.ReadAsByteArrayAsync().Result);
            var responseContent = (KeepAliveLogoutResponse?)jsonSerialiser.ReadObject(stream);

            if (responseContent == null) throw new InvalidDataException($"LOGIN_FAIL; " +
                $"Login Response from {client.BaseAddress} could not be read.");

            return responseContent;
        }
    }
}

