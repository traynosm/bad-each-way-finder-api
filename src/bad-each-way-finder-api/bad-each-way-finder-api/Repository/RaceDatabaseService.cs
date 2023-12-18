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
                .Include(r => r.Runners)
                .FirstOrDefault(r => r.SportsbookWinMarketId == race.SportsbookWinMarketId);

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
    }
}
