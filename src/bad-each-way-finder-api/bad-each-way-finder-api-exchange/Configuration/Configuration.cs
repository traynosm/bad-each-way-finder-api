using bad_each_way_finder_api_exchange;
using bad_each_way_finder_api_exchange.Client;
using bad_each_way_finder_api_exchange.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace bad_each_way_finder_api.Configuration
{
    public static partial class Configuration
    {
        public static void ConfigureExchange(this IServiceCollection services)
        {
            services.AddSingleton<IExchangeHandler, ExchangeHandler>();
            services.AddSingleton<IExchangeClient, ExchangeClient>();
        }
    }
}
