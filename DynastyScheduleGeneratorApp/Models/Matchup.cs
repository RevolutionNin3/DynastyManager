using System.Collections.Generic;

namespace DynastyManagerApp.Models
{
    public class Matchup
    {
        public Matchup()
        {
            Players = new List<string>();
        }

        public string Team1 { get; set; }

        public string Team2 { get; set; }

        public int Id { get; set; }

        public int RosterId { get; set; }

        public List<string> Players { get; set; }

        public int Points { get; set; }

        public int? CustomPoints { get; set; }

        public int Week { get; set; }
    }
}
