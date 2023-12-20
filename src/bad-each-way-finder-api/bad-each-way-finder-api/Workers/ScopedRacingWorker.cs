﻿using bad_each_way_finder_api_domain.CommonInterfaces;

namespace bad_each_way_finder_api.Workers
{
    public sealed class ScopedRacingWorker : IScopedRacingWorker
    {
        private readonly IRaceService _raceService;

        public ScopedRacingWorker(IRaceService raceService)
        {
            _raceService = raceService;
        }

        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            await _raceService.BuildRaces();
        }
    }
}
