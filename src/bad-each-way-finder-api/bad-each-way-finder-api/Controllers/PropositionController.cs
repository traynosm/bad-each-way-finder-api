using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.DomainModel;
using bad_each_way_finder_api_domain.DTO;
using bad_each_way_finder_api_domain.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace bad_each_way_finder_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropositionController : ControllerBase
    {
        private readonly IRaceService _raceService;
        private readonly ITokenService _tokenService;

        public PropositionController(IRaceService raceService, ITokenService tokenService)
        {
            _raceService = raceService;   
            _tokenService = tokenService;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string token)
        {
            if (!_tokenService.ValidateToken(token))
            {
                return BadRequest("Invalid Token");
            }
            var races = await _raceService.BuildRaces();
            var livePropositions = _raceService.DetermineLivePropositions(races);
            var raisedPropositions = _raceService.GetRaisedPropositionsForTimeRange(
                BetFairQueryExtensions.RacingQueryTimeRange());
            var dto = new RacesAndPropositionsDTO()
            {
                Races = races,
                LivePropositions = livePropositions,
                RaisedPropositions = raisedPropositions
            };
            return Ok(dto);
        }
    }
}
