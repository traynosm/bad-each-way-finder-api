using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bad_each_way_finder_api_domain.Sportsbook
{
    public class RunnerDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int selectionId { get; set; }
        public string selectionName { get; set; }
        public int runnerOrder { get; set; }
        [NotMapped]
        public WinRunnerOdds winRunnerOdds { get; set; }
        [NotMapped]
        public EachwayRunnerOdds eachwayRunnerOdds { get; set; }
        public double handicap { get; set; }
        public string runnerStatus { get; set; }
    }
}
