using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace bad_each_way_finder_api.Configuration
{
    public static partial class Configuration
    {
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddSingleton<IDatabaseService, DatabaseService>();
        }
    }
}