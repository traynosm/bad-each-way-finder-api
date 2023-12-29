using bad_each_way_finder_api.Areas.Identity.Data;
using bad_each_way_finder_api.Settings;
using bad_each_way_finder_api_domain.CommonInterfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace bad_each_way_finder_api.Workers
{
    public class RacingWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IOptions<RaceWorkerSettings> _options;
        public RacingWorker(IServiceProvider serviceProvider, IOptions<RaceWorkerSettings> options) 
        {
            _serviceProvider = serviceProvider;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (IServiceScope scope = _serviceProvider.CreateScope())
                {
                    IScopedRacingWorker scopedRacingWorker =
                        scope.ServiceProvider.GetRequiredService<IScopedRacingWorker>();

                    await scopedRacingWorker.DoWorkAsync(stoppingToken);
                }

                await Task.Delay(_options.Value.WorkerRefreshRateMs, stoppingToken);
            }
        }
    }
}
