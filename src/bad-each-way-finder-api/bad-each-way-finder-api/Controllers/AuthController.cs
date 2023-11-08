using bad_each_way_finder_api_auth.Interfaces;
using bad_each_way_finder_api_domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace bad_each_way_finder_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthHandler _authHandler;
        public AuthController(IAuthHandler authHandler) 
        { 
            _authHandler = authHandler;
        }
        [HttpGet]
        public ActionResult Login()
        {
            var loginSuccess = _authHandler.Login(
                "", "", Bookmaker.BetfairExchange);
            return Ok(loginSuccess);
        }
    }
}
