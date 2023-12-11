namespace bad_each_way_finder_api_domain.Identity
{
    public class AppUser
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public IEnumerable<string>? Roles { get; set; }

    }
}
