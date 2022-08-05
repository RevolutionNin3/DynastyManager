using System.Collections.Generic;

namespace DynastyManagerApp.Models
{
    public class Conference
    {
        public Conference()
        {
            Teams = new List<Team>();
        }

        public string Name { get; set; }

        public string Id { get; set; }

        public List<Team> Teams { get; set; }

    }
}
