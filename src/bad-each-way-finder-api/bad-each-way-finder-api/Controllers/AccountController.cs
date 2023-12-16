using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.DomainModel;
using bad_each_way_finder_api_domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace bad_each_way_finder_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet("GetAccountPropositions/{userName}")]
        public IActionResult GetAccountPropositions(string userName)
        {
            return Ok(_accountService.GetAccountPropositions(userName));
        }

        [HttpPost]
        [Route("PostSaveProposition")]
        public IActionResult PostSaveProposition(SavedPropositionDto savedPropositionDto)
        {
            if (savedPropositionDto == null)
            {
                return BadRequest("");
            }

            var accountPropositions = _accountService.SaveAndGetAccountPropositions(savedPropositionDto);

            return Ok(accountPropositions);
        }

        [HttpPost]
        [Route("RemoveAccountProposition")]
        public IActionResult RemoveAccountProposition(SavedPropositionDto savedPropositionDto)
        {
            if (savedPropositionDto == null)
            {
                return BadRequest("");
            }

            var accountPropositions = _accountService.DeleteAndGetAccountPropositions(savedPropositionDto);

            return Ok(accountPropositions);
        }


    }
}
