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
        private readonly IRaceService _propositionService;

        public PropositionController(IRaceService propositionService)
        {
            _propositionService = propositionService;   
        }

        [HttpGet]
        public IActionResult Get()
        {
            var races = _propositionService.BuildRaces();
            var propositions = _propositionService.DeterminePropositions(races);
            var savedPropositions = _propositionService.GetTodaysSavedPropositions();
            var dto = new RacesAndPropositionsDTO()
            {
                Races = races,
                Propositions = propositions,
                SavedPropositions = savedPropositions
            };
            return Ok(dto);
        }
    }
}
