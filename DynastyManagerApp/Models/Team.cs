namespace DynastyManagerApp.Models
{
    public class Team
    {
        public string Name { get; set; }

        public decimal Fpts { get; set; }

        public decimal MaxPtsFor { get; set; }

        public int Wins { get; set; }

        public int Losses { get; set; }

        public int Ties { get; set; }

        public int Standing { get; set; }

        public int ConferenceStanding { get; set; }
    }
}
