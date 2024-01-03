using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.DomainModel;
using bad_each_way_finder_api_domain.DTO;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace bad_each_way_finder_api.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ITokenService _tokenService;

        public AccountController(IAccountService accountService, ITokenService tokenService)
        {
            _accountService = accountService;
            _tokenService = tokenService;

        }
        [HttpGet("GetAccountPropositions/{userName}/{token}")]
        public IActionResult GetAccountPropositions(string userName, string token)
        {
            try
            {
                if (!_tokenService.ValidateToken(token))
                {
                    return BadRequest("Invalid Token");
                }
                return Ok(_accountService.GetAccountPropositions(userName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("PostRaisedProposition")]
        public IActionResult PostRaisedProposition(RaisedPropositionDto raisedPropositionDto)
        {
            try
            {
                if (raisedPropositionDto == null)
                {
                    return BadRequest("Data is null");
                }

                if (!_tokenService.ValidateToken(raisedPropositionDto.Token))
                {
                    return BadRequest("Invalid Token");
                }

                var accountPropositions = _accountService.SaveAccountProposition(raisedPropositionDto);

                return Ok(accountPropositions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("RemoveAccountProposition")]
        public IActionResult RemoveAccountProposition(RaisedPropositionDto raisedPropositionDto)
        {
            try
            {
                if (raisedPropositionDto == null)
                {
                    return BadRequest("Data is null");
                }

                if (!_tokenService.ValidateToken(raisedPropositionDto.Token))
                {
                    return BadRequest("Invalid Token");
                }

                var accountPropositions = _accountService.DeleteAndGetAccountPropositions(raisedPropositionDto);

                return Ok(accountPropositions);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
