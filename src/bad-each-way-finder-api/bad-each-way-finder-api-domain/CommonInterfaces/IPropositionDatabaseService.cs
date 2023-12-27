using bad_each_way_finder_api_domain.DomainModel;
using bad_each_way_finder_api_domain.Exchange;

namespace bad_each_way_finder_api_domain.CommonInterfaces
{
    public interface IPropositionDatabaseService
    {
        void AddProposition(Proposition proposition);
        List<Proposition> GetRaisedPropositionsForTimeRange(TimeRange timeRange);
        Proposition GetSingleProposition(string runnerName, double winOdds, string eventId);
    }
}
