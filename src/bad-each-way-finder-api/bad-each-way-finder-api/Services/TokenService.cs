using bad_each_way_finder_api_domain.CommonInterfaces;
using Microsoft.Extensions.Caching.Memory;

namespace bad_each_way_finder_api.Services
{
    public class TokenService : ITokenService
    {
        private readonly IMemoryCache _memoryCache;
        public TokenService(IMemoryCache memoryCache) 
        { 
            _memoryCache = memoryCache;
        }

        public void AddToken(string token, DateTime expiration)
        {
            if(string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Cannot add token, string is null/empty");
                return;
            }
            if(expiration < DateTime.Now)
            {
                Console.WriteLine("Cannot add token, expiration time is in the past");
                return;
            }

            _memoryCache.Set(token, expiration);
        }

        public bool ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Cannot validate token, string is null/empty");
                return false;
            }

            if (_memoryCache.Get(token) == null)
            {
                return false;
            }
            return true;
        }

        public void RemoveToken(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                Console.WriteLine("Cannot Remove token, string is null/empty");
                return;
            }

            _memoryCache.Remove(token);
        }
    }
}
