using DynastyManagerApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynastyManagerApp.Helpers
{
    public class DraftLottoHelper
    {
        static readonly Random random = new Random();

        public static async Task<DraftDetails> GenerateDraftOrderAsync(long leagueId)
        {
            var league = await SleeperHelper.GetSleeperDataAsync(leagueId);
            var draftDetails = GenerateDraftOrder(league);

            ExportToCsvSimple(draftDetails.DraftOrder);

            return draftDetails;
        }

        //public static async Task<List<string>> GenerateDraftOrderAsync(long leagueId)
        //{
        //    var league = await SleeperHelper.GetSleeperDataAsync(leagueId);

        //    var now = DateTime.Now.ToString();
        //    now = now.Replace('/', '-');
        //    now = now.Replace(':', '_');
        //    var filePath = @$"C:\Dev\Dynasty\Draft Lotto\DynastyScheduleGenerator Export {now}.csv";

        //    var draftOrder = new List<string>();
        //    var csv = new StringBuilder();

        //    int i = 0;
        //    while (i < 10000)
        //    {
        //        draftOrder = GenerateDraftOrder(league);
        //        ExportToCsvSimple(filePath, csv, draftOrder);
        //        i++;
        //    }

        //    return draftOrder;
        //}

        private static int GenerateRandomNumber(int maxCount, int? exclude = null)
        {
            var randomNumber = random.Next(maxCount);

            if (randomNumber == exclude)
            {
                return GenerateRandomNumber(maxCount, exclude);
            }

            return randomNumber;
        }

        private static void ExportToCsvSimple(List<string> strings)
        {
            var csv = new StringBuilder();
            var now = DateTime.Now.ToString();
            now = now.Replace('/', '-');
            now = now.Replace(':', '_');
            var filePath = @$"C:\Dev\Dynasty\Draft Lotto\DynastyScheduleGenerator Export {now}.csv";

            var i = 1;
            foreach (var s in strings)
            {
                csv.AppendLine(i.ToString() + ". " + s);
                i++;
            }

            File.WriteAllText(filePath, csv.ToString());
        }

        private static void ExportToCsvSimple(string filePath, StringBuilder csv, List<string> strings)
        {
            var i = 1;
            foreach (var s in strings)
            {
                csv.AppendLine(i.ToString() + "|" + s);
                i++;
            }

            File.WriteAllText(filePath, csv.ToString());
        }

        private static DraftDetails GenerateDraftOrder(League league)
        {
            var draftDetails = new DraftDetails();
            var teams = new List<Team>();
            var draftPercentages = new List<int>
            {
                30,
                22,
                16,
                13,
                9,
                5,
                3,
                2
            };
            var draftArray = new List<string>();
            var prestigiousConferenceSorted = new List<Team>();
            var diamondConferenceSorted = new List<Team>();

            prestigiousConferenceSorted = league.Conferences[0].Teams.OrderBy(t => t.MaxPtsFor).ToList();
            diamondConferenceSorted = league.Conferences[1].Teams.OrderBy(t => t.MaxPtsFor).ToList();

            // Get list of all teams that made the playoffs
            var playoffRosterIds = league.WinnersBracket.Where(t => t.r == 1).Select(t => t.t1).ToList();
            playoffRosterIds.AddRange(league.WinnersBracket.Where(t => t.r == 1).Select(t => t.t2));

            // Remove teams that made the playoffs
            prestigiousConferenceSorted.RemoveAll(t => playoffRosterIds.Contains(t.RosterId));
            diamondConferenceSorted.RemoveAll(t => playoffRosterIds.Contains(t.RosterId));

            teams.AddRange(prestigiousConferenceSorted);
            teams.AddRange(diamondConferenceSorted);

            // Re-sort remaining teams by MPF + AdditionalPoints
            teams = teams.OrderBy(t => t.MaxPtsFor).ToList();
            draftDetails.MpfOrder = teams.Select(t => t.Name).ToList();

            foreach (var team in teams)
            {
                if (draftDetails.Adjustments.ContainsKey(team.Name))
                {
                    team.AdditionalPoints = draftDetails.Adjustments[team.Name];
                }

                if (team.AdditionalPoints > 0)
                {
                    team.Name = team.Name + " (" + team.MaxPtsFor + " + " + team.AdditionalPoints + ")";
                }
                else
                {
                    team.Name = team.Name + " (" + team.MaxPtsFor + ")";
                }
            }

            teams = teams.OrderBy(t => t.MaxPtsFor + t.AdditionalPoints).ToList();
            draftDetails.AdjustedOrder = teams.Select(t => t.Name).ToList();

            if (teams.Count == draftPercentages.Count)
            {
                for (int i = 0; i < teams.Count; i++)
                {
                    int j = 0;
                    while (j < draftPercentages[i])
                    {
                        draftArray.Add(teams[i].Name);
                        j++;
                    }
                }
            }
            else
            {
                return draftDetails;
            }

            for (int i = 0; i < 8; i++)
            {
                if (i == 2 && draftDetails.DraftOrder.Contains(teams[0].Name) == false)
                {
                    draftDetails.DraftOrder.Add(teams[0].Name);
                    draftArray.RemoveAll(d => d == teams[0].Name);
                    continue;
                }

                if (i == 3 && draftDetails.DraftOrder.Contains(teams[1].Name) == false)
                {
                    draftDetails.DraftOrder.Add(teams[1].Name);
                    draftArray.RemoveAll(d => d == teams[1].Name);
                    continue;
                }

                if (i == 4 && draftDetails.DraftOrder.Contains(teams[2].Name) == false)
                {
                    draftDetails.DraftOrder.Add(teams[2].Name);
                    draftArray.RemoveAll(d => d == teams[2].Name);
                    continue;
                }

                if (i == 5 && draftDetails.DraftOrder.Contains(teams[3].Name) == false)
                {
                    draftDetails.DraftOrder.Add(teams[3].Name);
                    draftArray.RemoveAll(d => d == teams[3].Name);
                    continue;
                }

                if (i == 6 && draftDetails.DraftOrder.Contains(teams[4].Name) == false)
                {
                    draftDetails.DraftOrder.Add(teams[4].Name);
                    draftArray.RemoveAll(d => d == teams[4].Name);
                    continue;
                }

                var random = GenerateRandomNumber(draftArray.Count);
                var team = draftArray[random];
                draftArray.RemoveAll(d => d == team);
                draftDetails.DraftOrder.Add(team);
            }

            return draftDetails;
        }
    }
}
