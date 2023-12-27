using bad_each_way_finder_api_domain.DomainModel;

namespace bad_each_way_finder_api_domain.DTO
{
    public class RacesAndPropositionsDTO
    {
        public List<Proposition> LivePropositions { get; set; }
        public List<Proposition> RaisedPropositions { get; set; }
        public List<Race> Races { get; set; }
    }
}
