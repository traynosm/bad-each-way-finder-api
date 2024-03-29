﻿using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.DTO;
using bad_each_way_finder_api_domain.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace bad_each_way_finder_api.Controllers
{
    [Route("api/v1/[controller]")]
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
            try
            {
                if (!_tokenService.ValidateToken(token))
                {
                    return BadRequest("Invalid Token");
                }
                var races = await _raceService.BuildRaces();
                var livePropositions = _raceService.DetermineLivePropositions(races);
                var newlyRaisedPropositions = livePropositions.Where(p => p.IsNewlyRaised).ToList();   
                var raisedPropositions = _raceService.GetRaisedPropositionsForTimeRange(
                    BetFairQueryExtensions.RacingQueryTimeRange());

                var dto = new RacesAndPropositionsDTO()
                {
                    Races = races,
                    LivePropositions = livePropositions,
                    RaisedPropositions = raisedPropositions,
                    NewlyRaisedPropositions = newlyRaisedPropositions 
                };
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
