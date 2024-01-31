namespace bad_each_way_finder_api_domain.Sportsbook
{
    public class Rule4Deduction
    {
        public int Id { get; set; }
        public double deduction { get; set; }
        public DateTime timeFrom { get; set; }
        public DateTime timeTo { get; set; }
        public string deductionPriceType { get; set; }
    }
}
