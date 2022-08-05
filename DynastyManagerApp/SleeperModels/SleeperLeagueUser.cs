namespace DynastyManagerApp.SleeperModels
{
    class SleeperLeagueUser
    {
        public long user_id { get; set; }

        public string display_name { get; set; }

        public SleeperMetadata metadata { get; set; }
    }
}
