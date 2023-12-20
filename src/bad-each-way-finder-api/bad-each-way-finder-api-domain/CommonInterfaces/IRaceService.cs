using bad_each_way_finder_api_domain.DomainModel;

namespace bad_each_way_finder_api_domain.CommonInterfaces
{
    public interface IRaceService
    {
        Task<List<Race>> BuildRaces();
        List<Proposition> DeterminePropositions(List<Race> races);
        List<Proposition> GetTodaysSavedPropositions();
    }
}
