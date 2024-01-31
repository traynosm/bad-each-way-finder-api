using bad_each_way_finder_api_sportsbook;
using bad_each_way_finder_api_sportsbook.Client;
using bad_each_way_finder_api_sportsbook.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace bad_each_way_finder_api.Configuration
{
    public static partial class Configuration
    {
        public static void ConfigureSportsbook(this IServiceCollection services)
        {
            services.AddSingleton<ISportsbookClient, SportsbookClient>();
            services.AddSingleton<ISportsbookHandler, SportsbookHandler>();
        }
    }
}
