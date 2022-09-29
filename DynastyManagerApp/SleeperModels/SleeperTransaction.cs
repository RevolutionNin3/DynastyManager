using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynastyManagerApp.SleeperModels
{
    class SleeperTransaction
    {
        public string type { get; set; }

        public string transaction_id { get; set; }

        public int[] roster_ids { get; set; }

        public string leg { get; set; }

        public string creator { get; set; }

        public int[] consentor_ids { get; set; }

        //public List<SleeperDraftPick> draft_picks { get; set; }

        //public List<SleeperWaiverBudget> waiver_budget { get; set; }

        //public List<SleeperDrops> drops { get; set; }

        //public List<SleeperAdds> adds { get; set; } 
    }
}
