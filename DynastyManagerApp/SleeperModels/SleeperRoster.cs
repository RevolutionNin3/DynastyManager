namespace DynastyManagerApp.SleeperModels
{
    class SleeperRoster
    {
        public long owner_id { get; set; }

        public int roster_id { get; set; }

        public SleeperRosterSettings settings {get; set;}
    }
}
