using bad_each_way_finder_api_domain.DomainModel;
using bad_each_way_finder_api_domain.DTO;

namespace bad_each_way_finder_api_domain.CommonInterfaces
{
    public interface IAccountService
    {
        List<Proposition> GetAccountPropositions(string userName);
        List<Proposition> SaveAndGetAccountPropositions(SavedPropositionDto savedPropositionDto);
        List<Proposition> DeleteAndGetAccountPropositions(SavedPropositionDto savedPropositionDto);
    }
}
