using DynastyManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace DynastyManagerApp.Helpers
{
    class ScheduleGeneratorHelper
    {
        int _retryCount = 0;
        const int _gameTotal = 7;
        const int _weekTotal = 13;
        static int[] _conferenceWeeks = { 0, 3, 6, 10, 11, 12, 13 };
        static readonly Random random = new Random();

        public List<Week> GenerateSchedule(League league)
        {
            var weeks = new List<Week>();
            var prestigious = league.Conferences.First(c => c.Name == "The Prestigious Conference").Teams.Select(t => t.Name).ToList();
            var diamond = league.Conferences.First(c => c.Name == "The Diamond Conference").Teams.Select(t => t.Name).ToList();

            // 1. Generate all intra-conference matchups (round robin)
            var prestigiousMatchups = GenerateRoundRobin(prestigious);
            var diamondMatchups = GenerateRoundRobin(diamond);

            // 2. Generate all possible cross-conference matchups
            var crossConferenceMatchups = new List<(string, string)>();
            foreach (var p in prestigious)
                foreach (var d in diamond)
                    crossConferenceMatchups.Add((p, d));
            Shuffle(crossConferenceMatchups);

            // 3. Assign matchups to weeks
            int totalWeeks = _weekTotal + 1;
            var weekMatchups = new List<List<Matchup>>(Enumerable.Range(0, totalWeeks).Select(_ => new List<Matchup>()));

            // Conference weeks
            foreach (var week in _conferenceWeeks)
            {
                // Each conference gets its own set of matchups for this week
                if (prestigiousMatchups.Count > 0)
                {
                    var matches = prestigiousMatchups[0];
                    prestigiousMatchups.RemoveAt(0);
                    weekMatchups[week].AddRange(matches.Select(m => new Matchup { Team1 = m.Item1, Team2 = m.Item2 }));
                }
                if (diamondMatchups.Count > 0)
                {
                    var matches = diamondMatchups[0];
                    diamondMatchups.RemoveAt(0);
                    weekMatchups[week].AddRange(matches.Select(m => new Matchup { Team1 = m.Item1, Team2 = m.Item2 }));
                }
            }

            // Cross-conference weeks
            for (int week = 0; week < totalWeeks; week++)
            {
                if (_conferenceWeeks.Contains(week)) continue;

                var scheduledPrestigious = new HashSet<string>();
                var scheduledDiamond = new HashSet<string>();
                var toRemove = new List<int>();

                for (int i = 0; i < crossConferenceMatchups.Count; i++)
                {
                    var (p, d) = crossConferenceMatchups[i];
                    if (!scheduledPrestigious.Contains(p) && !scheduledDiamond.Contains(d))
                    {
                        weekMatchups[week].Add(new Matchup { Team1 = p, Team2 = d });
                        scheduledPrestigious.Add(p);
                        scheduledDiamond.Add(d);
                        toRemove.Add(i);

                        // If all teams are scheduled, break
                        if (scheduledPrestigious.Count == prestigious.Count && scheduledDiamond.Count == diamond.Count)
                            break;
                    }
                }

                // Remove used matchups in reverse order to keep indices valid
                toRemove.Reverse();
                foreach (var idx in toRemove)
                    crossConferenceMatchups.RemoveAt(idx);
            }


            // Build Week objects
            for (int week = 0; week < totalWeeks; week++)
            {
                weeks.Add(new Week
                {
                    Id = week,
                    Matchups = weekMatchups[week]
                });
            }

            return weeks;
        }

        // Helper: round robin generator for a list of teams
        private List<List<(string, string)>> GenerateRoundRobin(List<string> teams)
        {
            var n = teams.Count;
            var rounds = n - 1;
            var result = new List<List<(string, string)>>();

            var list = new List<string>(teams);
            if (n % 2 == 1) list.Add("BYE"); // Odd number of teams

            n = list.Count;
            for (int round = 0; round < rounds; round++)
            {
                var pairs = new List<(string, string)>();
                for (int i = 0; i < n / 2; i++)
                {
                    if (list[i] != "BYE" && list[n - 1 - i] != "BYE")
                        pairs.Add((list[i], list[n - 1 - i]));
                }
                result.Add(pairs);

                // Rotate
                var last = list[n - 1];
                list.RemoveAt(n - 1);
                list.Insert(1, last);
            }
            return result;
        }

        // Helper: Fisher-Yates shuffle
        private void Shuffle<T>(IList<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int j = random.Next(i + 1);
                var temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }


        // Commenting out my original function. Lets see if vibe-coding fixes this.
        //public List<Week> GenerateSchedule(League league)
        //{
        //    var weekCount = 0;
        //    var weeks = new List<Week>();

        //    while (weekCount <= _weekTotal)
        //    {
        //        var week = GenerateWeek(league, weeks, weekCount);
        //        weeks.Add(week);
        //        weekCount++;
        //    }

        //    return weeks;
        //}

        public Week GenerateWeek(League league, List<Week> weeks, int weekCount)
        {
            var week = new Week();
            week.Id = weekCount;
            week.Matchups = GenerateMatchups(league, weeks, weekCount);

            return week;
        }

        public List<Matchup> GenerateMatchups(League league, List<Week> weeks, int weekCount)
        {
            var generate = true;
            List<Matchup> matchups = null;

            while (generate)
            {
                var gameCount = 0;
                var tryAgain = false;
                var prestigiousConf = league.Conferences.FirstOrDefault(c => c.Name == "The Prestigious Conference").Teams.Select(c => c.Name).ToList();
                var diamondConf = league.Conferences.FirstOrDefault(c => c.Name == "The Diamond Conference").Teams.Select(c => c.Name).ToList();
                _retryCount++;

                // This is super janky, but this prevents an issue where valid matchups can't be found in later weeks if earlier weeks contains specific matchups.
                if(_retryCount > 100000)
                {
                    _retryCount = 0;
                    GenerateSchedule(league);
                    break;
                }

                matchups = new List<Matchup>();

                while (gameCount <= _gameTotal)
                {
                    var matchup = GenerateMatchup(prestigiousConf, diamondConf, weekCount);

                    if (prestigiousConf.Contains(matchup.Team1))
                    {
                        prestigiousConf.Remove(matchup.Team1);
                    }
                    else
                    {
                        diamondConf.Remove(matchup.Team1);
                    }

                    if (prestigiousConf.Contains(matchup.Team2))
                    {
                        prestigiousConf.Remove(matchup.Team2);
                    }
                    else
                    {
                        diamondConf.Remove(matchup.Team2);
                    }

                    matchup.Id = gameCount;
                    matchups.Add(matchup);

                    gameCount++;
                }

                foreach (var matchup in matchups)
                {
                    if (MatchupExists(weeks, matchup.Team1, matchup.Team2))
                    { 
                        tryAgain = true;
                        break;
                    }
                }

                if (!tryAgain)
                {
                    generate = false;
                }
            }

            return matchups;
        }

        public Matchup GenerateMatchup(List<string> prestigiousConf, List<string> diamondConf, int weekCount)
        {
            var matchup = new Matchup();

            if (_conferenceWeeks.Contains(weekCount))
            {
                if (prestigiousConf.Count > 0)
                {
                    var prestigiousIndex = GenerateRandomNumber(prestigiousConf.Count);
                    var prestigiousIndex2 = GenerateRandomNumber(prestigiousConf.Count, prestigiousIndex);
                    matchup.Team1 = prestigiousConf[prestigiousIndex];
                    matchup.Team2 = prestigiousConf[prestigiousIndex2];
                }
                else
                {
                    var diamondIndex = GenerateRandomNumber(diamondConf.Count);
                    var diamondIndex2 = GenerateRandomNumber(diamondConf.Count, diamondIndex);
                    matchup.Team1 = diamondConf[diamondIndex];
                    matchup.Team2 = diamondConf[diamondIndex2];
                }
            }
            else
            {
                var prestigiousIndex = GenerateRandomNumber(prestigiousConf.Count);
                var diamondIndex = GenerateRandomNumber(diamondConf.Count);
                matchup.Team1 = prestigiousConf[prestigiousIndex];
                matchup.Team2 = diamondConf[diamondIndex];
            }

            return matchup;
        }

        public bool MatchupExists(List<Week> weeks, string team1, string team2)
        {
            foreach (var week in weeks)
            {
                if (week.Matchups.Any(mu => (mu.Team1 == team1 && mu.Team2 == team2) || (mu.Team1 == team2 && mu.Team2 == team1)))
                {
                    return true;
                }
            }

            return false;
        }

        public int GenerateRandomNumber(int maxCount, int? exclude = null)
        {
            var randomNumber = random.Next(maxCount);

            if (randomNumber == exclude)
            {
                return GenerateRandomNumber(maxCount, exclude);
            }

            return randomNumber;
        }
    }
}
