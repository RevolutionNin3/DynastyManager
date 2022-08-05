using DynastyManagerApp.Models;
using DynastyManagerApp.SleeperModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DynastyManagerApp.Helpers
{
    public class SleeperHelper
    {
        const string _sURL = "https://api.sleeper.app";
        const string _playersJsonPath = @"C:\Dev\Dynasty\Players.json";
        static readonly HttpClient client = new HttpClient();

        public static async Task<League> GetSleeperDataAsync(long leagueId)
        {
            var league = new League();

            var leagueResponse = await client.GetAsync($"{_sURL}/v1/league/{leagueId}");
            leagueResponse.EnsureSuccessStatusCode();
            var leagueResponseRaw = await leagueResponse.Content.ReadAsStringAsync();
            var sleeperLeague = JsonConvert.DeserializeObject<SleeperLeague>(leagueResponseRaw);

            league.Name = sleeperLeague.name;
            league.Season = sleeperLeague.season;
            league.Status = sleeperLeague.status;

            var conference1 = new Conference
            {
                Name = sleeperLeague.metadata.division_1,
                Id = "1"
            };
            var conference2 = new Conference
            {
                Name = sleeperLeague.metadata.division_2,
                Id = "2"
            };

            league.Conferences.Add(conference1);
            league.Conferences.Add(conference2);

            var leagueUsersResponse = await client.GetAsync($"{_sURL}/v1/league/{leagueId}/users");
            leagueUsersResponse.EnsureSuccessStatusCode();
            var leagueUsersResponseRaw = await leagueUsersResponse.Content.ReadAsStringAsync();
            var sleeperLeagueUsers = JsonConvert.DeserializeObject<List<SleeperLeagueUser>>(leagueUsersResponseRaw);

            var leagueRosterResponse = await client.GetAsync($"{_sURL}/v1/league/{leagueId}/rosters");
            leagueRosterResponse.EnsureSuccessStatusCode();
            var leagueRosterResponseRaw = await leagueRosterResponse.Content.ReadAsStringAsync();
            var sleeperRosters = JsonConvert.DeserializeObject<List<SleeperRoster>>(leagueRosterResponseRaw);

            foreach (var user in sleeperLeagueUsers)
            {
                var sleeperRoster = sleeperRosters.FirstOrDefault(sr => sr.owner_id == user.user_id);
                var conference = league.Conferences.FirstOrDefault(c => c.Id == sleeperRoster.settings.division);

                var team = new Team();

                if (!string.IsNullOrEmpty(user.metadata.team_name))
                {
                    team.Name = user.metadata.team_name;
                }
                else
                {
                    team.Name = user.display_name;
                }

                team.Wins = Convert.ToInt32(sleeperRoster.settings.wins);
                team.Losses = Convert.ToInt32(sleeperRoster.settings.losses);
                team.Ties = Convert.ToInt32(sleeperRoster.settings.ties);
                team.Fpts = CalculateFpts(sleeperRoster.settings.fpts, sleeperRoster.settings.fpts_decimal);

                conference.Teams.Add(team);
            }

            return league;
        }

        public static async Task GetPlayerDataAsync()
        {
            var playersResponse = await client.GetAsync($"{_sURL}/v1/players/nfl");
            playersResponse.EnsureSuccessStatusCode();

            using (var fileStream = new FileStream(_playersJsonPath, FileMode.OpenOrCreate))
            {
                await playersResponse.Content.CopyToAsync(fileStream);
            }
        }

        public static async Task<List<Matchup>> GetAllMatchupsAsync(long leagueId)
        {
            // TODO: refactor this function to execute the tasks as a group instead of one by one
            
            var matchups = new List<Matchup>();
            var seasonWeek = 14;
            
            while(seasonWeek > 0)
            {
                var matchupResponse = await client.GetAsync($"{_sURL}/v1/league/{leagueId}/matchups/{seasonWeek}");
                matchupResponse.EnsureSuccessStatusCode();
                var matchupResponseRaw = await matchupResponse.Content.ReadAsStringAsync();
                var sleeperMatchup = JsonConvert.DeserializeObject<SleeperMatchup>(matchupResponseRaw);

                var matchup = new Matchup
                {
                    Id = sleeperMatchup.roster_id,
                    CustomPoints = sleeperMatchup.custom_points,
                    Players = sleeperMatchup.players,
                    Points = sleeperMatchup.points,
                    RosterId = sleeperMatchup.roster_id,
                    Week = seasonWeek
                };

                matchups.Add(matchup);
                seasonWeek--;
            }

            return matchups;
        }

        private static decimal CalculateFpts(string fptsString, string fpts_decimalString)
        {
            var fpts = Convert.ToDecimal(fptsString);
            var fptsDecimal = Convert.ToDecimal(fpts_decimalString);
            fptsDecimal = fptsDecimal * .01M;

            return fpts + fptsDecimal;
        }
    }
}
