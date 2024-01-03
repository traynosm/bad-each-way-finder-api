using bad_each_way_finder_api_auth.Interfaces;
using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace bad_each_way_finder_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthHandler _authHandler;
        private readonly ITokenService _tokenService;

        public AuthController(IAuthHandler authHandler, ITokenService tokenService) 
        { 
            _authHandler = authHandler;
            _tokenService = tokenService;
        }
        [HttpGet]
        public ActionResult Login(string token)
        {
            try
            {
                if (!_tokenService.ValidateToken(token))
                {
                    return BadRequest("Invalid Token");
                };
                var loginSuccess = _authHandler.Login(
                    "", "", Bookmaker.BetfairExchange);
                return Ok(loginSuccess);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
