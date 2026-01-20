using System.Collections.Generic;

namespace DynastyManagerApp.Models
{
    public class DraftDetails
    {
        public List<string> DraftOrder = new List<string>();

        public List<string> MpfOrder = new List<string>();

        public List<string> AdjustedOrder = new List<string>();

        public Dictionary<string, decimal> Adjustments = new Dictionary<string, decimal>
        {
            { "Blue Barracudas", 23.46m },
            { "BrianMcgrail", 7.5m }
        };
    }
}
