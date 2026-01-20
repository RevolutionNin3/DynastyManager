public class LeagueListItem
{
    public long LeagueId { get; set; }
    public string LeagueName { get; set; }
    public override string ToString() => LeagueName;
}