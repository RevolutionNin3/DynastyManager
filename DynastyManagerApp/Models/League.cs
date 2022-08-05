using System.Collections.Generic;

namespace DynastyManagerApp.Models
{
    public class League
    { 
        public League()
        {
            Conferences = new List<Conference>();
            Schedule = new Schedule();
        }

        public string Name { get; set; }

        public List<Conference> Conferences { get; set; }

        public Schedule Schedule {get; set;}

        public string Season { get; set; }

        public string Status { get; set; }
    }
}
