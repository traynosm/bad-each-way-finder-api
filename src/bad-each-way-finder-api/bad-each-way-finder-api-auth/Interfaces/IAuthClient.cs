using bad_each_way_finder_api_domain.Enums;
using bad_each_way_finder_api_domain.Exchange;

namespace bad_each_way_finder_api_auth.Interfaces
{
    public interface IAuthClient
    {
        KeepAliveLogoutResponse Login(string username, string password, string appKey, Bookmaker bookmaker = Bookmaker.BetfairSportsbook);
    }
}