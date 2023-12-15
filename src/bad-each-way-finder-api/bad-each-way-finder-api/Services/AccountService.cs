using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.DomainModel;
using bad_each_way_finder_api_domain.DTO;

namespace bad_each_way_finder_api.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IAccountDatabaseService _accountDatabaseService;
        private readonly IPropositionDatabaseService _propositionDatabaseService;

        public AccountService(ILogger<AccountService> logger, IAccountDatabaseService accountDatabaseService, 
            IPropositionDatabaseService propositionDatabaseService)
        {
            _logger = logger;
            _accountDatabaseService = accountDatabaseService;
            _propositionDatabaseService = propositionDatabaseService;
        }

        public List<Proposition> GetAccountPropositions(string userName)
        {
            var account = _accountDatabaseService.GetOrAddAccount(userName);

            return account.AccountPropositions;
        }

        public List<Proposition> SaveAndGetAccountPropositions(SavedPropositionDto savedPropositionDto)
        {
            var account = _accountDatabaseService.GetOrAddAccount(savedPropositionDto.IdentityUserName);

            var savedProposition = _propositionDatabaseService.GetSingleProposition(
                savedPropositionDto.RunnerName,
                savedPropositionDto.WinRunnerOddsDecimal,
                savedPropositionDto.EventId);

            account.AccountPropositions.Add(savedProposition);

            _accountDatabaseService.AddAccountProposition(account);

            return account.AccountPropositions;
        }
    }

}
