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

        public static async Task<List<string>> GenerateDraftOrderAsync(long leagueId)
        {
            var league = await SleeperHelper.GetSleeperDataAsync(leagueId);
            var draftOrder = GenerateDraftOrder(league);

            ExportToCsvSimple(draftOrder);

            return draftOrder;
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

        private static List<string> GenerateDraftOrder(League league)
        {
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
            var draftOrder = new List<string>();
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

            // Re-sort remaining teams by MPF
            teams = teams.OrderBy(t => t.MaxPtsFor).ToList();

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
                return new List<string>();
            }

            for (int i = 0; i < 8; i++)
            {
                if (i == 2 && draftOrder.Contains(teams[0].Name) == false)
                {
                    draftOrder.Add(teams[0].Name);
                    draftArray.RemoveAll(d => d == teams[0].Name);
                    continue;
                }

                if (i == 3 && draftOrder.Contains(teams[1].Name) == false)
                {
                    draftOrder.Add(teams[1].Name);
                    draftArray.RemoveAll(d => d == teams[1].Name);
                    continue;
                }

                if (i == 4 && draftOrder.Contains(teams[2].Name) == false)
                {
                    draftOrder.Add(teams[2].Name);
                    draftArray.RemoveAll(d => d == teams[2].Name);
                    continue;
                }

                if (i == 5 && draftOrder.Contains(teams[3].Name) == false)
                {
                    draftOrder.Add(teams[3].Name);
                    draftArray.RemoveAll(d => d == teams[3].Name);
                    continue;
                }

                if (i == 6 && draftOrder.Contains(teams[4].Name) == false)
                {
                    draftOrder.Add(teams[4].Name);
                    draftArray.RemoveAll(d => d == teams[4].Name);
                    continue;
                }

                var random = GenerateRandomNumber(draftArray.Count);
                var team = draftArray[random];
                draftArray.RemoveAll(d => d == team);
                draftOrder.Add(team);
            }

            return draftOrder;
        }
    }
}
