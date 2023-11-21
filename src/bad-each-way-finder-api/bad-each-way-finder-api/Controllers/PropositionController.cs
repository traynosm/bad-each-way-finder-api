using bad_each_way_finder_api_domain.CommonInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace bad_each_way_finder_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropositionController : ControllerBase
    {
        private readonly IPropositionService _propositionService;

        public PropositionController(IPropositionService propositionService)
        {
            _propositionService = propositionService;   
        }

        [HttpGet]
        public IActionResult Get()
        {
            var races = _propositionService.BuildRaces();
            return Ok(races);
        }
    }
}
