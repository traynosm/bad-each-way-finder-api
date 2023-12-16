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

            //Check do we already have this proposition on the account
            if(account.AccountPropositions
                .Any(p => 
                p.RunnerName == savedProposition.RunnerName &&
                p.WinRunnerOddsDecimal == savedProposition.WinRunnerOddsDecimal &&
                p.EventId == savedProposition.EventId)) 
            { 
                return account.AccountPropositions;
            }
            account.AccountPropositions.Add(savedProposition);

            _accountDatabaseService.AddAccountProposition(account);

            return account.AccountPropositions;
        }

        public List<Proposition> DeleteAndGetAccountPropositions(SavedPropositionDto savedPropositionDto)
        {
            var account = _accountDatabaseService.GetOrAddAccount(savedPropositionDto.IdentityUserName);

            var propositionToRemove = account.AccountPropositions
                .FirstOrDefault(p =>
                    p.RunnerName == savedPropositionDto.RunnerName &&
                    p.WinRunnerOddsDecimal == savedPropositionDto.WinRunnerOddsDecimal &&
                    p.EventId == savedPropositionDto.EventId);

            account.AccountPropositions = account.AccountPropositions
                .Where(p => p != propositionToRemove)
                .ToList();

            _accountDatabaseService.UpdateAccountPropositions(account);

            return account.AccountPropositions;
        }
    }

}
