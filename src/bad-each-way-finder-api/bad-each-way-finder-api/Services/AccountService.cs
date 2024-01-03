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
            try
            {
                var account = _accountDatabaseService.GetOrAddAccount(userName);

                var todaysAccountPropositions = account.AccountPropositions
                    .Where(p => p.EventDateTime.Date == DateTime.Today.AddDays(0))
                    .ToList();

                foreach (var proposition in todaysAccountPropositions)
                {
                    try
                    {
                        var runnerInfo = _raceDatabaseService.GetRunnerInfo(
                            $"{proposition.EventId}{proposition.RunnerSelectionId}");

                        proposition.LatestWinPrice = runnerInfo.ExchangeWinPrice;
                        proposition.LatestPlacePrice = runnerInfo.ExchangePlacePrice;

                        if (runnerInfo.ExchangeWinPrice > 0)
                        {
                            proposition.LatestWinExpectedValue = proposition.WinRunnerOddsDecimal.ExpectedValue(
                                proposition.LatestWinPrice);
                        }

                        if (runnerInfo.ExchangePlacePrice > 0)
                        {
                            var placeExpectedValue = proposition.EachWayPlacePart.ExpectedValue(proposition.LatestPlacePrice);

                            proposition.LatestEachWayExpectedValue = (proposition.LatestWinExpectedValue + placeExpectedValue) / 2;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                }

                return account.AccountPropositions;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public List<Proposition> SaveAccountProposition(RaisedPropositionDto raisedPropositionDto)
        {
            try
            {
                var account = _accountDatabaseService.GetOrAddAccount(raisedPropositionDto.IdentityUserName);

                var existingProposition = _propositionDatabaseService.GetSingleProposition(
                    raisedPropositionDto.RunnerName,
                    raisedPropositionDto.WinRunnerOddsDecimal,
                    raisedPropositionDto.EventId);

                //Check do we already have this proposition on the account
                if (account.AccountPropositions
                    .Any(p =>
                    p.RunnerName == existingProposition.RunnerName &&
                    p.WinRunnerOddsDecimal == existingProposition.WinRunnerOddsDecimal &&
                    p.EventId == existingProposition.EventId))
                {
                    return account.AccountPropositions;
                }
                account.AccountPropositions.Add(existingProposition);

                _accountDatabaseService.AddAccountProposition(account);

                return account.AccountPropositions;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public List<Proposition> DeleteAndGetAccountPropositions(RaisedPropositionDto raisedPropositionDto)
        {
            try
            {
                var account = _accountDatabaseService.GetOrAddAccount(raisedPropositionDto.IdentityUserName);

                var propositionToRemove = account.AccountPropositions
                    .FirstOrDefault(p =>
                        p.RunnerName == raisedPropositionDto.RunnerName &&
                        p.WinRunnerOddsDecimal == raisedPropositionDto.WinRunnerOddsDecimal &&
                        p.EventId == raisedPropositionDto.EventId);

                account.AccountPropositions = account.AccountPropositions
                    .Where(p => p != propositionToRemove)
                    .ToList();

                _accountDatabaseService.UpdateAccountPropositions(account);

                return account.AccountPropositions;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
