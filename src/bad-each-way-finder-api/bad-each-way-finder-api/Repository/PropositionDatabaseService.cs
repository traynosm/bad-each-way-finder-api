using bad_each_way_finder_api.Areas.Identity.Data;
using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.DomainModel;

namespace bad_each_way_finder_api.Repository
{
    public class PropositionDatabaseService : DatabaseService, IPropositionDatabaseService
    {
        public PropositionDatabaseService(BadEachWayFinderApiContext context, ILogger<DatabaseService> logger) : 
            base(context, logger)
        {
        }

        public void AddProposition(Proposition proposition)
        {
            var savedProposition = _context.Propositions.FirstOrDefault(p => 
                p.WinRunnerOddsDecimal == proposition.WinRunnerOddsDecimal && 
                p.RunnerName == proposition.RunnerName &&
                p.EventId == proposition.EventId);

            if (savedProposition == null)
            {
                _context.Propositions.Add(proposition);
            }
            else
            {
                _context.Entry(savedProposition).CurrentValues.SetValues(proposition);
            }

            _context.SaveChanges();
        }

        public List<Proposition> GetTodaysSavedPropositions()
        {
            var savedPropositions = _context.Propositions
                .Where(p => p.EventDateTime.Date == DateTime.Today.AddDays(0))
                .ToList();

            return savedPropositions;
        }

        public Proposition GetSingleProposition(string runnerName, double winOdds, string eventId)
        {
            var proposition = _context.Propositions
                .FirstOrDefault(p =>
                p.RunnerName == runnerName &&
                p.WinRunnerOddsDecimal == winOdds &&
                p.EventId == eventId);

            if (proposition == null)
            {
                return new Proposition();
            }

            return proposition;
        }
    }
}
