using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_sportsbook.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace bad_each_way_finder_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SportsbookController : Controller
    {
        private readonly ISportsbookHandler _sportsbookHandler;
        private readonly ISportsbookDatabaseService _databaseService;
        private readonly ITokenService _tokenService;

        public SportsbookController(ISportsbookHandler sportsbookHandler, ISportsbookDatabaseService databaseService,
            ITokenService tokenService)
        {
            _sportsbookHandler = sportsbookHandler;
            _databaseService = databaseService;
            _tokenService = tokenService;
        }

        [HttpGet]
        [Route("GetSportsbookEventTypes/{token}")]
        public IActionResult GetSportsbookEventTypes(string token)
        {
            if (!_tokenService.ValidateToken(token))
            {
                return BadRequest("Invalid Token");
            };

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
        [Route("GetSportsbookEventsByEventType/{token}")]
        public IActionResult GetSportsbookEventsByEventType(string token)
        {
            if (!_tokenService.ValidateToken(token))
            {
                return BadRequest("Invalid Token");
            };

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
        [Route("GetMarketCatalogue/{token}")]
        public IActionResult GetMarketCatalogue(string token)
        {
            if (!_tokenService.ValidateToken(token))
            {
                return BadRequest("Invalid Token");
            };

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
        [Route("GetMarketPrices/{token}")]
        public IActionResult GetMarketPrices(string token)
        {
            if (!_tokenService.ValidateToken(token))
            {
                return BadRequest("Invalid Token");
            };

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
