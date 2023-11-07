using System;
using bad_each_way_finder_api_domain.Enums;

namespace bad_each_way_finder_api_auth.Interfaces
{
    public interface IAuthHandler
    {
        bool TryLogin(Bookmaker bookmaker);
        bool Login(string username, string password, Bookmaker bookmaker);
        bool SessionValid(Bookmaker bookmaker);

        public Dictionary<Bookmaker, string> SessionTokens { get; }
        public string AppKey { get; }
    }
}

