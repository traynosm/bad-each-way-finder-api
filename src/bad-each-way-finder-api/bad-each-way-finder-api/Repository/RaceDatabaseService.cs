using bad_each_way_finder_api.Areas.Identity.Data;
using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.DomainModel;
using bad_each_way_finder_domain.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace bad_each_way_finder_api.Repository
{
    public class RaceDatabaseService : DatabaseService, IRaceDatabaseService
    {
        public RaceDatabaseService(BadEachWayFinderApiContext context, ILogger<DatabaseService> logger) :
            base(context, logger)
        {

        }

        public void AddOrUpdateRace(Race race)
        {
            var existingRace = _context.Races
                .FirstOrDefault(r => r.SportsbookWinMarketId == race.SportsbookWinMarketId);

            foreach(var runnerInfo in race.Runners) 
            {
                var existingRunnerInfo = _context.RunnerInfos
                   .FirstOrDefault(r => r.Id == $"{race.EventId}{runnerInfo.RunnerSelectionId}");

                if(existingRunnerInfo == null)
                {
                    _context.RunnerInfos.Add(runnerInfo);
                }
                else
                {
                    _context.Entry(existingRunnerInfo).CurrentValues.SetValues(runnerInfo);
                }
            }

            if (existingRace == null)
            {
                _context.Races.Add(race);
            }
            else
            {
                _context.Entry(existingRace).CurrentValues.SetValues(race);
            }

            _context.SaveChanges();
        }

        public RunnerInfo GetRunnerInfo(string id)
        {
            var runnerInfo = _context.RunnerInfos.FirstOrDefault(r => r.Id == id);

            if (runnerInfo == null)
            {
                throw new InvalidOperationException();
            }

            return runnerInfo;
        }
    }
}
