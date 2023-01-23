using DynastyManagerApp.Helpers;
using System;
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
            leagueDropDown.SelectedIndex = 0;
        }

        private async void lottoGenerateButton_Click(object sender, EventArgs e)
        {
            var leagueId = Convert.ToInt64(leagueDropDown.SelectedItem);
            var teams = await DraftLottoHelper.GenerateDraftOrderAsync(leagueId);

            draftOrderTextBox.Text = DateTime.Now.ToString();
            var i = 1;
            foreach (var team in teams)
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

            var leagueId = Convert.ToInt64(leagueDropDown.SelectedItem);
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
            var leagueId = Convert.ToInt64(leagueDropDown.SelectedItem);
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
