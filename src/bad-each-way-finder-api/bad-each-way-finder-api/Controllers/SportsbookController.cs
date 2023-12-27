using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_sportsbook.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bad_each_way_finder_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsbookController : Controller
    {
        private readonly ISportsbookHandler _sportsbookHandler;
        private readonly ISportsbookDatabaseService _databaseService;

        public SportsbookController(ISportsbookHandler sportsbookHandler, ISportsbookDatabaseService databaseService)
        {
            _sportsbookHandler = sportsbookHandler;
            _databaseService = databaseService;
        }

        [HttpGet]
        [Route("GetSportsbookEventTypes")]
        public IActionResult GetSportsbookEventTypes()
        {
            var loginSuccess = _sportsbookHandler.TryLogin();

            if (loginSuccess)
            {
                var result = _sportsbookHandler.ListEventTypes();
                return Ok(result);
            }
            else
            {
                return BadRequest(string.Empty);
            }
        }

        [HttpGet]
        [Route("GetSportsbookEventsByEventType")]
        public IActionResult GetSportsbookEventsByEventType()
        {
            var loginSuccess = _sportsbookHandler.TryLogin();

            if (loginSuccess)
            {
                var result = _sportsbookHandler.ListEventsByEventType("7");
                return Ok(result);
            }
            else
            {
                return BadRequest(string.Empty);
            }
        }

        [HttpGet]
        [Route("GetMarketCatalogue")]
        public IActionResult GetMarketCatalogue()
        {
            var loginSuccess = _sportsbookHandler.TryLogin();

            if (loginSuccess)
            {
                var eventIds = _sportsbookHandler.ListEventsByEventType("7");
                var result = _sportsbookHandler.ListMarketCatalogues(eventIds.Select(
                    e => e.Id).ToHashSet());
                return Ok(result);
            }
            else
            {
                return BadRequest(string.Empty);
            }
        }

        [HttpGet]
        [Route("GetMarketPrices")]
        public IActionResult GetMarketPrices()
        {
            var loginSuccess = _sportsbookHandler.TryLogin();

            if (loginSuccess)
            {
                var eventIds = _sportsbookHandler.ListEventsByEventType("7");
                var eId = _sportsbookHandler.ListMarketCatalogues(eventIds.Select(
                    e => e.Id).ToHashSet());
                var result = _sportsbookHandler.ListPrices(eId.Select(
                    e => e.MarketId).ToList());
                _databaseService.AddOrUpdateMarketDetails(result);
                return Ok(result);
            }
            else
            {
                return BadRequest(string.Empty);
            }
        }
    }
}
