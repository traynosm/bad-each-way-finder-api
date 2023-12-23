using bad_each_way_finder_api_domain.CommonInterfaces;
using bad_each_way_finder_api_domain.DomainModel;
using bad_each_way_finder_api_domain.DTO;
using Microsoft.AspNetCore.Mvc;

namespace bad_each_way_finder_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropositionController : ControllerBase
    {
        private readonly IRaceService _raceService;

        public PropositionController(IRaceService raceService)
        {
            _raceService = raceService;   
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var races = await _raceService.BuildRaces();
            var livePropositions = _raceService.DetermineLivePropositions(races);
            var savedPropositions = _raceService.GetTodaysSavedPropositions();
            var dto = new RacesAndPropositionsDTO()
            {
                Races = races,
                LivePropositions = livePropositions,
                SavedPropositions = savedPropositions
            };
            return Ok(dto);
        }
    }
}
