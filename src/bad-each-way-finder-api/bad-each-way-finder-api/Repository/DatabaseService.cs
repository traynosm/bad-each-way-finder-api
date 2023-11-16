using bad_each_way_finder_api.Areas.Identity.Data;

namespace bad_each_way_finder_api.Repository
{
    public class DatabaseService
    {
        protected readonly ILogger<DatabaseService> _logger;
        protected readonly BadEachWayFinderApiContext _context;
        public DatabaseService(BadEachWayFinderApiContext context, ILogger<DatabaseService> logger)
        {
            _context = context;
            _logger = logger;
        }
    }
}
