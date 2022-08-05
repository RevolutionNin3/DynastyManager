using System.Collections.Generic;

namespace DynastyManagerApp.Models
{
    public class Week
    {
        public Week()
        {
            Matchups = new List<Matchup>();
        }

        public int Id { get; set; }

        public List<Matchup> Matchups { get; set; }
    }
}
