﻿using bad_each_way_finder_api_domain.DomainModel;

namespace bad_each_way_finder_api_domain.CommonInterfaces
{
    public interface IRaceDatabaseService
    {
        void AddOrUpdateRace(Race race);
        Race GetRace(string id);
        RunnerInfo GetRunnerInfo(string id);
    }
}
