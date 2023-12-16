using bad_each_way_finder_domain.DomainModel;

namespace bad_each_way_finder_api_domain.CommonInterfaces
{
    public interface IAccountDatabaseService
    {
        Account GetOrAddAccount(string IdentityUserName);

        void AddAccountProposition(Account account);
        void UpdateAccountPropositions(Account account);
    }
}
