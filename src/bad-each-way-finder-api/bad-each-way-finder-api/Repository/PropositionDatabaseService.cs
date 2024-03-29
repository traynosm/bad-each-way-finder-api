﻿using bad_each_way_finder_api.Areas.Identity.Data;
using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.DomainModel;
using bad_each_way_finder_api_domain.Exchange;

namespace bad_each_way_finder_api.Repository
{
    public class PropositionDatabaseService : DatabaseService, IPropositionDatabaseService
    {
        public PropositionDatabaseService(BadEachWayFinderApiContext context, ILogger<DatabaseService> logger) : 
            base(context, logger)
        {
        }

        public bool AddProposition(Proposition proposition)
        {
            bool isNew = false;
            
            var existingRaisedProposition = _context.Propositions.FirstOrDefault(p => 
                p.WinRunnerOddsDecimal == proposition.WinRunnerOddsDecimal && 
                p.RunnerName == proposition.RunnerName &&
                p.EventId == proposition.EventId);

            if (existingRaisedProposition == null)
            {
                _context.Propositions.Add(proposition);
                isNew = true;
            }
            else
            {
                _context.Entry(existingRaisedProposition).CurrentValues.SetValues(proposition);
            }

            _context.SaveChanges();
            return isNew;
        }

        public List<Proposition> GetRaisedPropositionsForTimeRange(TimeRange timeRange)
        {
            var raisedPropositions = _context.Propositions
                .Where(p => p.EventDateTime.Date >= timeRange.From && 
                            p.EventDateTime.Date <= timeRange.To)
                .ToList();

            return raisedPropositions;
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
