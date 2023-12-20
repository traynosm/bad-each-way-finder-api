using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.DomainModel;
using bad_each_way_finder_api_domain.DTO;
using bad_each_way_finder_api_domain.Extensions;

namespace bad_each_way_finder_api.Services
{
    public class AccountService : IAccountService
    {
        private readonly ILogger<AccountService> _logger;
        private readonly IAccountDatabaseService _accountDatabaseService;
        private readonly IPropositionDatabaseService _propositionDatabaseService;
        private readonly IRaceDatabaseService _raceDatabaseService;

        public AccountService(ILogger<AccountService> logger, IAccountDatabaseService accountDatabaseService, 
            IPropositionDatabaseService propositionDatabaseService, IRaceDatabaseService raceDatabaseService)
        {
            _logger = logger;
            _accountDatabaseService = accountDatabaseService;
            _propositionDatabaseService = propositionDatabaseService;
            _raceDatabaseService = raceDatabaseService;
        }

        public List<Proposition> GetAccountPropositions(string userName)
        {
            var account = _accountDatabaseService.GetOrAddAccount(userName);

            var todaysAccount = account.AccountPropositions.Where(p => p.EventDateTime.Date == DateTime.Today).ToList();

            foreach (var proposition in todaysAccount)
            {
                var runnerInfo = _raceDatabaseService.GetRunnerInfo($"{proposition.EventId}{proposition.RunnerSelectionId}");

                proposition.LatestWinPrice = runnerInfo.ExchangeWinPrice;
                proposition.LatestPlacePrice = runnerInfo.ExchangePlacePrice;
                proposition.LatestWinExpectedValue = proposition.WinRunnerOddsDecimal.ExpectedValue(proposition.LatestWinPrice);

                var placeExpectedValue = proposition.EachWayPlacePart.ExpectedValue(proposition.LatestPlacePrice);

                proposition.LatestEachWayExpectedValue = (proposition.LatestWinExpectedValue + placeExpectedValue) / 2;
            }

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
