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
            _memoryCache.Set(token, expiration);
        }

        public bool ValidateToken(string token)
        {
            if(_memoryCache.Get(token) == null)
            {
                return false;
            }
            return true;
        }

        public void RemoveToken(string token)
        {
            _memoryCache.Remove(token);
        }
    }
}
