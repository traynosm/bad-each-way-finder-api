using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_exchange.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bad_each_way_finder_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ExchangeController : ControllerBase
    {
        private readonly IExchangeHandler _exchangeHandler;
        private readonly IExchangeDatabaseService _databaseService;

        public ExchangeController(IExchangeHandler exchangeHandler, IExchangeDatabaseService databaseService)
        {
            _exchangeHandler = exchangeHandler;
            _databaseService = databaseService;
        }

        [HttpGet]
        [Route("GetExchangeEventTypes")]
        public IActionResult GetExchangeEventTypes()
        {
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
        [Route("GetExchangeEvents")]
        public IActionResult GetExchangeEvents()
        {
            var loginSuccess = _exchangeHandler.TryLogin();

            if (loginSuccess)
            {
                var result = _exchangeHandler.ListEvents();
                return Ok(result);
            }
            else
            {
                return BadRequest(string.Empty);
            }
        }

        [HttpGet]
        [Route("GetMarketCatalogues")]
        public IActionResult GetMarketCatalogues()
        {
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
        [Route("GetMarketBooks")]
        public IActionResult GetMarketBooks()
        {
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
