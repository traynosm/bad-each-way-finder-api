using bad_each_way_finder_api.Areas.Identity.Data;
using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.DomainModel;
using bad_each_way_finder_domain.DomainModel;
using Microsoft.EntityFrameworkCore;

namespace bad_each_way_finder_api.Repository
{
    public class AccountDatabaseService : DatabaseService, IAccountDatabaseService
    {
        public AccountDatabaseService(BadEachWayFinderApiContext context, ILogger<DatabaseService> logger) :
            base(context, logger)
        {
        }

        public Account GetOrAddAccount(string IdentityUserName)
        {
            var accountExists = _context.Accounts
                .Any(p => p.IdenityUserName == IdentityUserName);

            if (accountExists) 
            {
                var account = _context.Accounts
                    .Include(p => p.AccountPropositions)
                    .FirstOrDefault(p => p.IdenityUserName == IdentityUserName);

                return account;
            }

            var newAccount = new Account()
            {
                IdenityUserName = IdentityUserName,
                AccountPropositions = new List<Proposition>(),
            };

            _context.Accounts.Add(newAccount);
            _context.SaveChanges();
            return newAccount;
        }

        public void AddAccountProposition(Account account)
        {
            _context.Entry(account).CurrentValues.SetValues(account);

            _context.SaveChanges();    
        }
    }
}
