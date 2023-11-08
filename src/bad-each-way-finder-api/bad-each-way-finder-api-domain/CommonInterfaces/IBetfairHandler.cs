namespace bad_each_way_finder_api_domain.CommonInterfaces
{
    public interface IBetfairHandler
    {
        bool TryLogin();
        bool Login(string username = "", string password = "");
        bool SessionValid();
    }
}
