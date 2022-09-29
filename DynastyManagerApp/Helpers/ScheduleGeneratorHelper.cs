using DynastyManagerApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DynastyManagerApp.Helpers
{
    class ScheduleGeneratorHelper
    {
        const int _gameTotal = 7;
        const int _weekTotal = 13;
        static int[] _conferenceWeeks = { 0, 3, 6, 10, 11, 12, 13 };
        List<Matchup> _masterMatchupListPrestigious = new List<Matchup>();
        List<Matchup> _masterMatchupListDiamond = new List<Matchup>();
        List<Matchup> _masterMatchupListInterconference = new List<Matchup>();
        List<string> _masterTeamList = new List<string>();

        List<Week> weeks = new List<Week>();
        static readonly Random random = new Random();

        public List<Week> GenerateSchedule(League league)
        {
            var weekCount = 0;

            InitializeMasterMatchupLists(league.Conferences);

            while (weekCount <= _weekTotal)
            {
                var week = GenerateWeek(weekCount);
                weeks.Add(week);
                weekCount++;
            }

            return weeks;
        }

        private void InitializeMasterMatchupLists(List<Conference> conferences)
        {
            var allTeams = new List<Team>();

            foreach(var conference in conferences)
            {
                foreach(var team in conference.Teams)
                {
                    team.ConferenceId = conference.Id;
                    allTeams.Add(team);
                    foreach(var team2 in conference.Teams)
                    {
                        if (team.Name != team2.Name)
                        {
                            var matchup = new Matchup();
                            matchup.Team1 = team.Name;
                            matchup.Team2 = team2.Name;

                            if (conference.Name == "The Prestigious Conference")
                            {
                                if (!_masterMatchupListPrestigious.Any(m => (m.Team1 == matchup.Team1 && m.Team2 == matchup.Team2) || (m.Team2 == matchup.Team1 && m.Team1 == matchup.Team2)))
                                {
                                    _masterMatchupListPrestigious.Add(matchup);
                                }
                            }

                            if (conference.Name == "The Diamond Conference")
                            {
                                if (!_masterMatchupListDiamond.Any(m => (m.Team1 == matchup.Team1 && m.Team2 == matchup.Team2) || (m.Team2 == matchup.Team1 && m.Team1 == matchup.Team2)))
                                {
                                    _masterMatchupListDiamond.Add(matchup);
                                }
                            }
                        }
                    }
                }
            }

            foreach(var team in allTeams)
            {
                _masterTeamList.Add(team.Name);
                foreach(var team2 in allTeams)
                {
                    if (team.Name != team2.Name && team.ConferenceId != team2.ConferenceId)
                    {
                        var matchup = new Matchup();
                        matchup.Team1 = team.Name;
                        matchup.Team2 = team2.Name;

                        if(!_masterMatchupListInterconference.Any(m => (m.Team1 == matchup.Team1 && m.Team2 == matchup.Team2) || (m.Team2 == matchup.Team1 && m.Team1 == matchup.Team2)))
                        {
                            _masterMatchupListInterconference.Add(matchup);
                        }
                    }
                }
            }
        }

        public Week GenerateWeek(int weekCount)
        {
            var week = new Week();
            week.Id = weekCount;
            week.Matchups = GenerateMatchups(weekCount);

            return week;
        }

        public List<Matchup> GenerateMatchups(int weekCount)
        {
            var matchups = new List<Matchup>();
            var gameCount = 0;

            while (gameCount <= _gameTotal)
            {
                var matchup = GenerateMatchup(matchups, gameCount, weekCount);

                matchup.Id = gameCount;
                matchup.Week = weekCount;
                matchups.Add(matchup);

                gameCount++;
            }

            return matchups;
        }

        public Matchup GenerateMatchup(List<Matchup> matchups, int gameCount, int weekCount)
        {
            if (_conferenceWeeks.Contains(weekCount))
            {
                if (gameCount < 4) // First four games are prestigious games, last 4 are diamond games
                {
                    var potentialValidMatchups = GetAllValidMatchups(matchups, true, true, weekCount);
                    var matchupIndex = GenerateRandomNumber(potentialValidMatchups.Count - 1); // List counts aren't 0-based, but ElementAt is, so we want the RNG to be able to return 0.
                    return potentialValidMatchups.ElementAt(matchupIndex);
                }
                else
                {
                    var potentialValidMatchups = GetAllValidMatchups(matchups, true, false, weekCount);
                    var matchupIndex = GenerateRandomNumber(potentialValidMatchups.Count - 1);
                    return potentialValidMatchups.ElementAt(matchupIndex);
                }
            }
            else
            {
                var potentialValidMatchups = GetAllValidMatchups(matchups, false, false, weekCount);
                var matchupIndex = GenerateRandomNumber(potentialValidMatchups.Count - 1);
                return potentialValidMatchups.ElementAt(matchupIndex);
            }
        }

        private List<Matchup> GetAllValidMatchups(List<Matchup> matchups, bool conferenceWeek, bool prestigious, int weekCount)
        {
            var allPossibleMatchups = new List<Matchup>();  // All possible matchup combinations for each team based on prestigious and weekcount flags. 
            var allExistingMatchups = new List<Matchup>(); // All matchups that have already been created as a part of this schedule.
            var allAvailableMatchups = new List<Matchup>(); // All matchups that are still available for the random generator to select from.
            
            allExistingMatchups.AddRange(matchups);
            foreach (var week in weeks)
            {
                allExistingMatchups.AddRange(week.Matchups);
            }

            if (conferenceWeek)
            {
                if (prestigious)
                {
                    allPossibleMatchups.AddRange(_masterMatchupListPrestigious);
                }
                else
                {
                    allPossibleMatchups.AddRange(_masterMatchupListDiamond);
                }
            }
            else
            {
                allPossibleMatchups.AddRange(_masterMatchupListInterconference);
            }

            foreach (var possibleMatchup in allPossibleMatchups)
            {
                if (!allExistingMatchups.Any(m => 
                    (m.Team1 == possibleMatchup.Team1 && m.Team2 == possibleMatchup.Team2) || // Dont allow exact matchups that already exist in previous weeks
                    (m.Team2 == possibleMatchup.Team1 && m.Team1 == possibleMatchup.Team2))) 
                {
                    if(!matchups.Any(m => 
                        m.Team1 == possibleMatchup.Team1 ||
                        m.Team2 == possibleMatchup.Team1 ||
                        m.Team1 == possibleMatchup.Team2 ||
                        m.Team2 == possibleMatchup.Team2))
                    {
                        allAvailableMatchups.Add(possibleMatchup);
                    }
                }
            }

            foreach (var team in _masterTeamList)
            {
                if(allAvailableMatchups.Count(m => m.Team1 == team || m.Team2 == team) == 1) // If a team only has one available matchup left, return only that matchup so that it gets assigned right away
                {
                    return new List<Matchup>() { allAvailableMatchups.First(m => m.Team1 == team || m.Team2 == team) };
                }
            }

            return allAvailableMatchups;

        }

        public int GenerateRandomNumber(int maxCount, int? exclude = null)
        {
            if(maxCount < 0)
            {
                maxCount = 0;
            }

            var randomNumber = random.Next(maxCount);

            if (randomNumber == exclude)
            {
                return GenerateRandomNumber(maxCount, exclude);
            }

             return randomNumber;
        }
    }
}
