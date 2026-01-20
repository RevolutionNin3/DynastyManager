using DynastyManagerApp.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DynastyManagerApp
{
    public partial class DynastyManagerForm : Form
    {
        string scheduleText = string.Empty;

        public DynastyManagerForm()
        {
            InitializeComponent();
            leagueDropDown.DropDownStyle = ComboBoxStyle.DropDownList;

            var leagues = new List<LeagueListItem>
            {
                new LeagueListItem { LeagueId = 1222047909508300800, LeagueName = "PDDL 2025" },
                new LeagueListItem { LeagueId = 1048377510624485376, LeagueName = "PDDL 2024" },
                new LeagueListItem { LeagueId = 919783144960638976, LeagueName = "PDDL 2023" },
                new LeagueListItem { LeagueId = 784917561144922112, LeagueName = "PDDL 2022" },
                new LeagueListItem { LeagueId = 719015752841101312, LeagueName = "PDDL 2021" }
            };

            leagueDropDown.DataSource = leagues;
            leagueDropDown.DisplayMember = "LeagueName";
            leagueDropDown.ValueMember = "LeagueId";
            leagueDropDown.SelectedIndex = 0;
        }

        private async void lottoGenerateButton_Click(object sender, EventArgs e)
        {
            var leagueId = ((LeagueListItem)leagueDropDown.SelectedItem).LeagueId;
            var draftDetails = await DraftLottoHelper.GenerateDraftOrderAsync(leagueId);

            draftOrderTextBox.Text = DateTime.Now.ToString();

            draftOrderTextBox.Text = draftOrderTextBox.Text + Environment.NewLine;
            draftOrderTextBox.Text = draftOrderTextBox.Text + Environment.NewLine + "MPF Rankings";

            var i = 1;
            foreach (var team in draftDetails.MpfOrder)
            {
                draftOrderTextBox.Text = draftOrderTextBox.Text + Environment.NewLine + i + ". " + team;
                i++;
            }

            draftOrderTextBox.Text = draftOrderTextBox.Text + Environment.NewLine;
            draftOrderTextBox.Text = draftOrderTextBox.Text + Environment.NewLine + "Manual adjustments were made to the following teams MPF:";

            foreach (var adjustment in draftDetails.Adjustments)
            {
                draftOrderTextBox.Text = draftOrderTextBox.Text + Environment.NewLine + adjustment.Key + " + " + adjustment.Value;
            }

            draftOrderTextBox.Text = draftOrderTextBox.Text + Environment.NewLine;
            draftOrderTextBox.Text = draftOrderTextBox.Text + Environment.NewLine + "Adjusted MPF Rankings";
            i = 1;
            foreach (var team in draftDetails.AdjustedOrder)
            {
                draftOrderTextBox.Text = draftOrderTextBox.Text + Environment.NewLine + i + ". " + team;
                i++;
            }

            draftOrderTextBox.Text = draftOrderTextBox.Text + Environment.NewLine;
            draftOrderTextBox.Text = draftOrderTextBox.Text + Environment.NewLine + "DRAFT RESULTS";
            i = 1;
            foreach (var team in draftDetails.DraftOrder)
            {
                draftOrderTextBox.Text = draftOrderTextBox.Text + Environment.NewLine + i + ". " + team;
                i++;
            }
            draftOrderTextBox.Text = draftOrderTextBox.Text + Environment.NewLine + Environment.NewLine;
        }

        private void clearDraftLottoButton_Click(object sender, EventArgs e)
        {
            draftOrderTextBox.Text = string.Empty;
        }


        private async void leagueDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {
            leagueDataLabel.Text = "Getting league data...";

            var leagueId = ((LeagueListItem)leagueDropDown.SelectedItem).LeagueId;
            var league = await SleeperHelper.GetSleeperDataAsync(leagueId);

            leagueDataLabel.Text = $"{league.Season} - {league.Name} : {league.Status}";
        }

        private void clearScheduleButton_Click(object sender, EventArgs e)
        {
            scheduleText = string.Empty;
            scheduleDataGrid.Rows.Clear();
        }

        private async void scheduleGenerateButton_Click(object sender, EventArgs e)
        {
            scheduleText = string.Empty;
            scheduleDataGrid.Rows.Clear();
            var leagueId = ((LeagueListItem)leagueDropDown.SelectedItem).LeagueId;
            var league = await SleeperHelper.GetSleeperDataAsync(leagueId);

            var generatorHelper = new ScheduleGeneratorHelper();
            league.Schedule.Weeks.AddRange(generatorHelper.GenerateSchedule(league));

            scheduleText = scheduleText + "Week,Game,Team 1,Team 2";
            foreach (var week in league.Schedule.Weeks)
            {
                foreach (var matchup in week.Matchups)
                {
                    var newLine = $"{week.Id + 1},{matchup.Id + 1},{matchup.Team1},{matchup.Team2}";
                    scheduleText = scheduleText + Environment.NewLine + newLine;
                    scheduleDataGrid.Rows.Add($"{week.Id + 1}",$"{matchup.Id + 1}",matchup.Team1,matchup.Team2);
                }
            }
        }

        private void saveScheduleButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Comma Separated Values|*.csv";
            saveDialog.Title = "Save Schedule";
            saveDialog.FileName = "PDDL Schedule.csv";
            saveDialog.ShowDialog();

            if(saveDialog.FileName != "")
            {
                using (System.IO.FileStream fs = (System.IO.FileStream)saveDialog.OpenFile())
                {
                    fs.Write(Encoding.UTF8.GetBytes(scheduleText));
                }
            }
        }
    }
}
