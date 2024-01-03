using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_exchange.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace bad_each_way_finder_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ExchangeController : ControllerBase
    {
        private readonly IExchangeHandler _exchangeHandler;
        private readonly IExchangeDatabaseService _databaseService;
        private readonly ITokenService _tokenService;


        public ExchangeController(IExchangeHandler exchangeHandler, IExchangeDatabaseService databaseService,
            ITokenService tokenService)
        {
            _exchangeHandler = exchangeHandler;
            _databaseService = databaseService;
            _tokenService = tokenService;
        }

        [HttpGet]
        [Route("GetExchangeEventTypes/{token}")]
        public IActionResult GetExchangeEventTypes(string token)
        {
            if (!_tokenService.ValidateToken(token))
            {
                return BadRequest("Invalid Token");
            };

            var loginSuccess = _exchangeHandler.TryLogin();

            if (loginSuccess)
            {
                var result = _exchangeHandler.ListEventTypes();
                return Ok(result);
            }
            else
            {
                return BadRequest(string.Empty);
            }
        }

        [HttpGet]
        [Route("GetExchangeEvents/{token}")]
        public IActionResult GetExchangeEvents(string token)
        {
            if (!_tokenService.ValidateToken(token))
            {
                return BadRequest("Invalid Token");
            };

            var loginSuccess = _exchangeHandler.TryLogin();

            if (loginSuccess)
            {
                try
                {
                    var result = _exchangeHandler.ListEvents();
                    return Ok(result);
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            else
            {
                return BadRequest(string.Empty);
            }
        }

        [HttpGet]
        [Route("GetMarketCatalogues/{token}")]
        public IActionResult GetMarketCatalogues(string token)
        {
            if (!_tokenService.ValidateToken(token))
            {
                return BadRequest("Invalid Token");
            };

            var loginSuccess = _exchangeHandler.TryLogin();

            if (loginSuccess)
            {
                var result = _exchangeHandler.ListMarketCatalogues();
                _databaseService.AddOrUpdateMarketCatalogues(result);
                return Ok(result);
            }
            else
            {
                return BadRequest(string.Empty);
            }
        }
        [HttpGet]
        [Route("GetMarketBooks/{token}")]
        public IActionResult GetMarketBooks(string token)
        {
            if (!_tokenService.ValidateToken(token))
            {
                return BadRequest("Invalid Token");
            };

            var loginSuccess = _exchangeHandler.TryLogin();

            if (loginSuccess)
            {
                var marketCatalogues = _exchangeHandler.ListMarketCatalogues();
                var result = _exchangeHandler.ListMarketBooks(marketCatalogues
                    .Select(m => m.MarketId).ToList());
                _databaseService.AddOrUpdateMarketBooks(result);

                return Ok(result);
            }
            else
            {
                return BadRequest(string.Empty);
            }
        }
    }
}
