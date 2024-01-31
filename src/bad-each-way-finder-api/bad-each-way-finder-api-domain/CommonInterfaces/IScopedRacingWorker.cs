namespace bad_each_way_finder_api_domain.CommonInterfaces
{
    public interface IScopedRacingWorker
    {
        Task DoWorkAsync(CancellationToken stoppingToken);
    }
}
