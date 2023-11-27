using bad_each_way_finder_api_domain.DomainModel;

namespace bad_each_way_finder_api_domain.DTO
{
    public class RacesAndPropositionsDTO
    {
        public List<Proposition> Propositions { get; set; }
        public List<Race> Races { get; set; }
    }
}
