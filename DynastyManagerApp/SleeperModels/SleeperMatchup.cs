using System.Collections.Generic;

namespace DynastyManagerApp.SleeperModels
{
    public class SleeperMatchup
    {
        public List<string> starters { get; set; }

        public int roster_id { get; set; }

        public List<string> players { get; set; }

        public int matchup_id { get; set; }

        public int points { get; set; }

        public int? custom_points { get; set; }
    }
}
