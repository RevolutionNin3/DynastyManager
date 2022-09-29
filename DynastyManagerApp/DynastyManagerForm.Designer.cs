
namespace DynastyManagerApp
{
    partial class DynastyManagerForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DynastyManagerForm));
            this.label1 = new System.Windows.Forms.Label();
            this.draftOrderTextBox = new System.Windows.Forms.TextBox();
            this.lottoGenerateButton = new System.Windows.Forms.Button();
            this.clearDraftLottoButton = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.scheduleDataGrid = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.team1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.team2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveScheduleButton = new System.Windows.Forms.Button();
            this.scheduleGenerateButton = new System.Windows.Forms.Button();
            this.clearScheduleButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.leagueDropDown = new System.Windows.Forms.ComboBox();
            this.leagueDataLabel = new System.Windows.Forms.Label();
            this.saveScheduleDialog = new System.Windows.Forms.SaveFileDialog();
            this.Week = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Game = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.transactionTeamDropdown = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.PlayerColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TransactionColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.getTransactionsButton = new System.Windows.Forms.Button();
            this.refreshPlayersButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scheduleDataGrid)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Draft Lotto Generator";
            // 
            // draftOrderTextBox
            // 
            this.draftOrderTextBox.Location = new System.Drawing.Point(6, 21);
            this.draftOrderTextBox.Multiline = true;
            this.draftOrderTextBox.Name = "draftOrderTextBox";
            this.draftOrderTextBox.ReadOnly = true;
            this.draftOrderTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.draftOrderTextBox.Size = new System.Drawing.Size(509, 344);
            this.draftOrderTextBox.TabIndex = 1;
            // 
            // lottoGenerateButton
            // 
            this.lottoGenerateButton.Location = new System.Drawing.Point(440, 371);
            this.lottoGenerateButton.Name = "lottoGenerateButton";
            this.lottoGenerateButton.Size = new System.Drawing.Size(75, 23);
            this.lottoGenerateButton.TabIndex = 2;
            this.lottoGenerateButton.Text = "Generate";
            this.lottoGenerateButton.UseVisualStyleBackColor = true;
            this.lottoGenerateButton.Click += new System.EventHandler(this.lottoGenerateButton_Click);
            // 
            // clearDraftLottoButton
            // 
            this.clearDraftLottoButton.Location = new System.Drawing.Point(6, 371);
            this.clearDraftLottoButton.Name = "clearDraftLottoButton";
            this.clearDraftLottoButton.Size = new System.Drawing.Size(75, 23);
            this.clearDraftLottoButton.TabIndex = 3;
            this.clearDraftLottoButton.Text = "Clear";
            this.clearDraftLottoButton.UseVisualStyleBackColor = true;
            this.clearDraftLottoButton.Click += new System.EventHandler(this.clearDraftLottoButton_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(4, 84);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(529, 430);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.draftOrderTextBox);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.lottoGenerateButton);
            this.tabPage1.Controls.Add(this.clearDraftLottoButton);
            this.tabPage1.Location = new System.Drawing.Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(521, 402);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Draft Lotto Generator";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.scheduleDataGrid);
            this.tabPage2.Controls.Add(this.saveScheduleButton);
            this.tabPage2.Controls.Add(this.scheduleGenerateButton);
            this.tabPage2.Controls.Add(this.clearScheduleButton);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Location = new System.Drawing.Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(521, 402);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Schedule Generator";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // scheduleDataGrid
            // 
            this.scheduleDataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.scheduleDataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.team1,
            this.team2});
            this.scheduleDataGrid.Location = new System.Drawing.Point(6, 21);
            this.scheduleDataGrid.Name = "scheduleDataGrid";
            this.scheduleDataGrid.RowTemplate.Height = 25;
            this.scheduleDataGrid.Size = new System.Drawing.Size(509, 344);
            this.scheduleDataGrid.TabIndex = 7;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.HeaderText = "Week";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.Width = 50;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.HeaderText = "Game";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.Width = 50;
            // 
            // team1
            // 
            this.team1.HeaderText = "Team 1";
            this.team1.Name = "team1";
            this.team1.Width = 150;
            // 
            // team2
            // 
            this.team2.HeaderText = "Team 2";
            this.team2.Name = "team2";
            this.team2.Width = 150;
            // 
            // saveScheduleButton
            // 
            this.saveScheduleButton.Location = new System.Drawing.Point(215, 371);
            this.saveScheduleButton.Name = "saveScheduleButton";
            this.saveScheduleButton.Size = new System.Drawing.Size(75, 23);
            this.saveScheduleButton.TabIndex = 6;
            this.saveScheduleButton.Text = "Save";
            this.saveScheduleButton.UseVisualStyleBackColor = true;
            this.saveScheduleButton.Click += new System.EventHandler(this.saveScheduleButton_Click);
            // 
            // scheduleGenerateButton
            // 
            this.scheduleGenerateButton.Location = new System.Drawing.Point(440, 371);
            this.scheduleGenerateButton.Name = "scheduleGenerateButton";
            this.scheduleGenerateButton.Size = new System.Drawing.Size(75, 23);
            this.scheduleGenerateButton.TabIndex = 5;
            this.scheduleGenerateButton.Text = "Generate";
            this.scheduleGenerateButton.UseVisualStyleBackColor = true;
            this.scheduleGenerateButton.Click += new System.EventHandler(this.scheduleGenerateButton_Click);
            // 
            // clearScheduleButton
            // 
            this.clearScheduleButton.Location = new System.Drawing.Point(6, 371);
            this.clearScheduleButton.Name = "clearScheduleButton";
            this.clearScheduleButton.Size = new System.Drawing.Size(75, 23);
            this.clearScheduleButton.TabIndex = 4;
            this.clearScheduleButton.Text = "Clear";
            this.clearScheduleButton.UseVisualStyleBackColor = true;
            this.clearScheduleButton.Click += new System.EventHandler(this.clearScheduleButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(110, 15);
            this.label3.TabIndex = 2;
            this.label3.Text = "Schedule Generator";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "League";
            // 
            // leagueDropDown
            // 
            this.leagueDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.leagueDropDown.FormattingEnabled = true;
            this.leagueDropDown.Items.AddRange(new object[] {
            "784917561144922112",
            "719015752841101312"});
            this.leagueDropDown.Location = new System.Drawing.Point(4, 28);
            this.leagueDropDown.Name = "leagueDropDown";
            this.leagueDropDown.Size = new System.Drawing.Size(525, 23);
            this.leagueDropDown.TabIndex = 5;
            this.leagueDropDown.SelectedIndexChanged += new System.EventHandler(this.leagueDropDown_SelectedIndexChanged);
            // 
            // leagueDataLabel
            // 
            this.leagueDataLabel.AutoSize = true;
            this.leagueDataLabel.Location = new System.Drawing.Point(4, 54);
            this.leagueDataLabel.Name = "leagueDataLabel";
            this.leagueDataLabel.Size = new System.Drawing.Size(119, 15);
            this.leagueDataLabel.TabIndex = 6;
            this.leagueDataLabel.Text = "Getting league data...";
            // 
            // Week
            // 
            this.Week.HeaderText = "Week";
            this.Week.Name = "Week";
            // 
            // Game
            // 
            this.Game.HeaderText = "Game";
            this.Game.Name = "Game";
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.HeaderText = "Week";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.HeaderText = "Game";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.HeaderText = "Week";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.refreshPlayersButton);
            this.tabPage3.Controls.Add(this.getTransactionsButton);
            this.tabPage3.Controls.Add(this.dataGridView1);
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.transactionTeamDropdown);
            this.tabPage3.Location = new System.Drawing.Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(521, 402);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Transaction Lookup";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // transactionTeamDropdown
            // 
            this.transactionTeamDropdown.FormattingEnabled = true;
            this.transactionTeamDropdown.Location = new System.Drawing.Point(4, 27);
            this.transactionTeamDropdown.Name = "transactionTeamDropdown";
            this.transactionTeamDropdown.Size = new System.Drawing.Size(514, 23);
            this.transactionTeamDropdown.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "Team";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.PlayerColumn,
            this.TransactionColumn});
            this.dataGridView1.Location = new System.Drawing.Point(4, 56);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(514, 313);
            this.dataGridView1.TabIndex = 2;
            // 
            // PlayerColumn
            // 
            this.PlayerColumn.HeaderText = "Player";
            this.PlayerColumn.Name = "PlayerColumn";
            // 
            // TransactionColumn
            // 
            this.TransactionColumn.HeaderText = "Transaction";
            this.TransactionColumn.Name = "TransactionColumn";
            // 
            // getTransactionsButton
            // 
            this.getTransactionsButton.Location = new System.Drawing.Point(386, 375);
            this.getTransactionsButton.Name = "getTransactionsButton";
            this.getTransactionsButton.Size = new System.Drawing.Size(132, 23);
            this.getTransactionsButton.TabIndex = 3;
            this.getTransactionsButton.Text = "Get Transactions";
            this.getTransactionsButton.UseVisualStyleBackColor = true;
            // 
            // refreshPlayersButton
            // 
            this.refreshPlayersButton.Location = new System.Drawing.Point(4, 375);
            this.refreshPlayersButton.Name = "refreshPlayersButton";
            this.refreshPlayersButton.Size = new System.Drawing.Size(111, 23);
            this.refreshPlayersButton.TabIndex = 4;
            this.refreshPlayersButton.Text = "Refresh Players";
            this.refreshPlayersButton.UseVisualStyleBackColor = true;
            // 
            // DynastyManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(545, 518);
            this.Controls.Add(this.leagueDataLabel);
            this.Controls.Add(this.leagueDropDown);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DynastyManagerForm";
            this.Text = "DynastyManager";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scheduleDataGrid)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox draftOrderTextBox;
        private System.Windows.Forms.Button lottoGenerateButton;
        private System.Windows.Forms.Button clearDraftLottoButton;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox leagueDropDown;
        private System.Windows.Forms.Label leagueDataLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button clearScheduleButton;
        private System.Windows.Forms.Button scheduleGenerateButton;
        private System.Windows.Forms.Button saveScheduleButton;
        private System.Windows.Forms.SaveFileDialog saveScheduleDialog;
        private System.Windows.Forms.DataGridView scheduleDataGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn Week;
        private System.Windows.Forms.DataGridViewTextBoxColumn Game;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn team1;
        private System.Windows.Forms.DataGridViewTextBoxColumn team2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button refreshPlayersButton;
        private System.Windows.Forms.Button getTransactionsButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn PlayerColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn TransactionColumn;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox transactionTeamDropdown;
    }
}

