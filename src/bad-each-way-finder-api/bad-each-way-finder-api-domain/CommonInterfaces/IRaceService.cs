using bad_each_way_finder_api_domain.DomainModel;
using bad_each_way_finder_api_domain.Exchange;

namespace bad_each_way_finder_api_domain.CommonInterfaces
{
    public interface IRaceService
    {
        Task<List<Race>> BuildRaces();
        List<Proposition> DetermineLivePropositions(List<Race> races);
        List<Proposition> GetRaisedPropositionsForTimeRange(TimeRange timeRange);
    }
}
