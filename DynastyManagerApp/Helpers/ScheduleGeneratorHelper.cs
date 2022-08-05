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
            var weekCount = 0;
            var weeks = new List<Week>();

            while (weekCount <= _weekTotal)
            {
                var week = GenerateWeek(league, weeks, weekCount);
                weeks.Add(week);
                weekCount++;
            }

            return weeks;
        }

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
