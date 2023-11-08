using bad_each_way_finder_api_exchange.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace bad_each_way_finder_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PropositionController : ControllerBase
    {
        private readonly IExchangeHandler _exchangeHandler;

        public PropositionController(IExchangeHandler exchangeHandler)
        {
            _exchangeHandler = exchangeHandler;
        }

        [HttpGet]
        public ActionResult Login()
        {
            var loginSuccess = _exchangeHandler.TryLogin();

            if (loginSuccess)
            {
                var eventTypes = _exchangeHandler.ListEventTypes();
                return Ok(eventTypes);
            }
            else
            {
                return BadRequest(string.Empty);
            }
        }
    }
}
