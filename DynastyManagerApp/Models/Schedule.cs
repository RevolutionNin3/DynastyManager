using System.Collections.Generic;

namespace DynastyManagerApp.Models
{
    public class Schedule
    {
        public Schedule()
        {
            Weeks = new List<Week>();
        }

        public List<Week> Weeks { get; set; }
    }
}
