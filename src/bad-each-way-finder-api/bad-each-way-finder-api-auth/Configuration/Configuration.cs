using bad_each_way_finder_api_auth.Clients;
using bad_each_way_finder_api_auth.Interfaces;
using Betfair.ExchangeComparison.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace bad_each_way_finder_api.Configuration
{
    public static partial class Configuration
    {
        public static void ConfigureAuth(this IServiceCollection services)
        {
            services.AddTransient<IAuthClient, AuthClient>();
            services.AddTransient<IAuthHandler, AuthHandler>();
        }
    }
}
