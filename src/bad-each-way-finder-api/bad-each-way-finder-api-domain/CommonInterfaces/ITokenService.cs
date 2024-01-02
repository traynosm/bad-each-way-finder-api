namespace bad_each_way_finder_api_domain.CommonInterfaces
{
    public interface ITokenService
    {
        void AddToken(string token, DateTime expiration);
        bool ValidateToken(string token);
    }
}
