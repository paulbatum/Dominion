namespace Dominion.AIWorkbench
{
    partial class SimulationForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.gbCards = new System.Windows.Forms.GroupBox();
            this.btnRandomCards = new System.Windows.Forms.Button();
            this.lbSelectedCards = new System.Windows.Forms.ListBox();
            this.btnUnselectCard = new System.Windows.Forms.Button();
            this.btnSelectCard = new System.Windows.Forms.Button();
            this.lbAllCards = new System.Windows.Forms.ListBox();
            this.gbPlayers = new System.Windows.Forms.GroupBox();
            this.btnPlayerSettings = new System.Windows.Forms.Button();
            this.btnRemovePlayer = new System.Windows.Forms.Button();
            this.btnAddPlayer = new System.Windows.Forms.Button();
            this.lbPlayers = new System.Windows.Forms.ListBox();
            this.cbPlayers = new System.Windows.Forms.ComboBox();
            this.gbSimulation = new System.Windows.Forms.GroupBox();
            this.txtOutputFilename = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnRun = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.nudGameCount = new System.Windows.Forms.NumericUpDown();
            this.gbResults = new System.Windows.Forms.GroupBox();
            this.lvResults = new System.Windows.Forms.ListView();
            this.colPlayer = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colWinPercentage = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colTotalScore = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label2 = new System.Windows.Forms.Label();
            this.pbProgress = new System.Windows.Forms.ProgressBar();
            this.flowLayoutPanel1.SuspendLayout();
            this.gbCards.SuspendLayout();
            this.gbPlayers.SuspendLayout();
            this.gbSimulation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGameCount)).BeginInit();
            this.gbResults.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.gbCards);
            this.flowLayoutPanel1.Controls.Add(this.gbPlayers);
            this.flowLayoutPanel1.Controls.Add(this.gbSimulation);
            this.flowLayoutPanel1.Controls.Add(this.gbResults);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(821, 330);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // gbCards
            // 
            this.gbCards.Controls.Add(this.btnRandomCards);
            this.gbCards.Controls.Add(this.lbSelectedCards);
            this.gbCards.Controls.Add(this.btnUnselectCard);
            this.gbCards.Controls.Add(this.btnSelectCard);
            this.gbCards.Controls.Add(this.lbAllCards);
            this.gbCards.Location = new System.Drawing.Point(3, 3);
            this.gbCards.Name = "gbCards";
            this.gbCards.Size = new System.Drawing.Size(375, 188);
            this.gbCards.TabIndex = 0;
            this.gbCards.TabStop = false;
            this.gbCards.Text = "Cards";
            // 
            // btnRandomCards
            // 
            this.btnRandomCards.Location = new System.Drawing.Point(159, 125);
            this.btnRandomCards.Name = "btnRandomCards";
            this.btnRandomCards.Size = new System.Drawing.Size(53, 23);
            this.btnRandomCards.TabIndex = 4;
            this.btnRandomCards.Text = "Rnd 10";
            this.btnRandomCards.UseVisualStyleBackColor = true;
            this.btnRandomCards.Click += new System.EventHandler(this.btnRandomCards_Click);
            // 
            // lbSelectedCards
            // 
            this.lbSelectedCards.FormattingEnabled = true;
            this.lbSelectedCards.Location = new System.Drawing.Point(218, 19);
            this.lbSelectedCards.Name = "lbSelectedCards";
            this.lbSelectedCards.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbSelectedCards.Size = new System.Drawing.Size(142, 160);
            this.lbSelectedCards.TabIndex = 3;
            this.lbSelectedCards.DoubleClick += new System.EventHandler(this.lbSelectedCards_DoubleClick);
            // 
            // btnUnselectCard
            // 
            this.btnUnselectCard.Location = new System.Drawing.Point(157, 98);
            this.btnUnselectCard.Name = "btnUnselectCard";
            this.btnUnselectCard.Size = new System.Drawing.Size(55, 23);
            this.btnUnselectCard.TabIndex = 2;
            this.btnUnselectCard.Text = "<<";
            this.btnUnselectCard.UseVisualStyleBackColor = true;
            this.btnUnselectCard.Click += new System.EventHandler(this.btnUnselectCard_Click);
            // 
            // btnSelectCard
            // 
            this.btnSelectCard.Location = new System.Drawing.Point(157, 69);
            this.btnSelectCard.Name = "btnSelectCard";
            this.btnSelectCard.Size = new System.Drawing.Size(55, 23);
            this.btnSelectCard.TabIndex = 1;
            this.btnSelectCard.Text = ">>";
            this.btnSelectCard.UseVisualStyleBackColor = true;
            this.btnSelectCard.Click += new System.EventHandler(this.btnSelectCard_Click);
            // 
            // lbAllCards
            // 
            this.lbAllCards.FormattingEnabled = true;
            this.lbAllCards.Location = new System.Drawing.Point(9, 19);
            this.lbAllCards.Name = "lbAllCards";
            this.lbAllCards.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbAllCards.Size = new System.Drawing.Size(142, 160);
            this.lbAllCards.TabIndex = 0;
            this.lbAllCards.DoubleClick += new System.EventHandler(this.lbAllCards_DoubleClick);
            // 
            // gbPlayers
            // 
            this.gbPlayers.Controls.Add(this.btnPlayerSettings);
            this.gbPlayers.Controls.Add(this.btnRemovePlayer);
            this.gbPlayers.Controls.Add(this.btnAddPlayer);
            this.gbPlayers.Controls.Add(this.lbPlayers);
            this.gbPlayers.Controls.Add(this.cbPlayers);
            this.gbPlayers.Location = new System.Drawing.Point(3, 197);
            this.gbPlayers.Name = "gbPlayers";
            this.gbPlayers.Size = new System.Drawing.Size(375, 126);
            this.gbPlayers.TabIndex = 1;
            this.gbPlayers.TabStop = false;
            this.gbPlayers.Text = "Players";
            // 
            // btnPlayerSettings
            // 
            this.btnPlayerSettings.Location = new System.Drawing.Point(285, 19);
            this.btnPlayerSettings.Name = "btnPlayerSettings";
            this.btnPlayerSettings.Size = new System.Drawing.Size(75, 23);
            this.btnPlayerSettings.TabIndex = 4;
            this.btnPlayerSettings.Text = "Settings";
            this.btnPlayerSettings.UseVisualStyleBackColor = true;
            // 
            // btnRemovePlayer
            // 
            this.btnRemovePlayer.Location = new System.Drawing.Point(285, 47);
            this.btnRemovePlayer.Name = "btnRemovePlayer";
            this.btnRemovePlayer.Size = new System.Drawing.Size(75, 23);
            this.btnRemovePlayer.TabIndex = 3;
            this.btnRemovePlayer.Text = "Remove";
            this.btnRemovePlayer.UseVisualStyleBackColor = true;
            this.btnRemovePlayer.Click += new System.EventHandler(this.btnRemovePlayer_Click);
            // 
            // btnAddPlayer
            // 
            this.btnAddPlayer.Location = new System.Drawing.Point(10, 47);
            this.btnAddPlayer.Name = "btnAddPlayer";
            this.btnAddPlayer.Size = new System.Drawing.Size(75, 23);
            this.btnAddPlayer.TabIndex = 2;
            this.btnAddPlayer.Text = "Add";
            this.btnAddPlayer.UseVisualStyleBackColor = true;
            this.btnAddPlayer.Click += new System.EventHandler(this.btnAddPlayer_Click);
            // 
            // lbPlayers
            // 
            this.lbPlayers.FormattingEnabled = true;
            this.lbPlayers.Location = new System.Drawing.Point(159, 19);
            this.lbPlayers.Name = "lbPlayers";
            this.lbPlayers.Size = new System.Drawing.Size(120, 95);
            this.lbPlayers.TabIndex = 1;
            // 
            // cbPlayers
            // 
            this.cbPlayers.FormattingEnabled = true;
            this.cbPlayers.Location = new System.Drawing.Point(9, 19);
            this.cbPlayers.Name = "cbPlayers";
            this.cbPlayers.Size = new System.Drawing.Size(142, 21);
            this.cbPlayers.TabIndex = 0;
            // 
            // gbSimulation
            // 
            this.gbSimulation.Controls.Add(this.txtOutputFilename);
            this.gbSimulation.Controls.Add(this.label3);
            this.gbSimulation.Controls.Add(this.btnRun);
            this.gbSimulation.Controls.Add(this.label1);
            this.gbSimulation.Controls.Add(this.nudGameCount);
            this.gbSimulation.Location = new System.Drawing.Point(384, 3);
            this.gbSimulation.Name = "gbSimulation";
            this.gbSimulation.Size = new System.Drawing.Size(437, 90);
            this.gbSimulation.TabIndex = 2;
            this.gbSimulation.TabStop = false;
            this.gbSimulation.Text = "Simulation";
            // 
            // txtOutputFilename
            // 
            this.txtOutputFilename.Location = new System.Drawing.Point(109, 45);
            this.txtOutputFilename.Name = "txtOutputFilename";
            this.txtOutputFilename.Size = new System.Drawing.Size(201, 20);
            this.txtOutputFilename.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Output name:";
            // 
            // btnRun
            // 
            this.btnRun.Location = new System.Drawing.Point(235, 15);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 23);
            this.btnRun.TabIndex = 2;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number of games:";
            // 
            // nudGameCount
            // 
            this.nudGameCount.Location = new System.Drawing.Point(109, 18);
            this.nudGameCount.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this.nudGameCount.Name = "nudGameCount";
            this.nudGameCount.Size = new System.Drawing.Size(120, 20);
            this.nudGameCount.TabIndex = 0;
            this.nudGameCount.ThousandsSeparator = true;
            // 
            // gbResults
            // 
            this.gbResults.Controls.Add(this.lvResults);
            this.gbResults.Controls.Add(this.label2);
            this.gbResults.Controls.Add(this.pbProgress);
            this.gbResults.Location = new System.Drawing.Point(384, 99);
            this.gbResults.Name = "gbResults";
            this.gbResults.Size = new System.Drawing.Size(437, 206);
            this.gbResults.TabIndex = 3;
            this.gbResults.TabStop = false;
            this.gbResults.Text = "Results";
            // 
            // lvResults
            // 
            this.lvResults.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colPlayer,
            this.colWinPercentage,
            this.colTotalScore});
            this.lvResults.Location = new System.Drawing.Point(6, 64);
            this.lvResults.Name = "lvResults";
            this.lvResults.Size = new System.Drawing.Size(425, 183);
            this.lvResults.TabIndex = 2;
            this.lvResults.UseCompatibleStateImageBehavior = false;
            this.lvResults.View = System.Windows.Forms.View.Details;
            // 
            // colPlayer
            // 
            this.colPlayer.Text = "Player";
            this.colPlayer.Width = 261;
            // 
            // colWinPercentage
            // 
            this.colWinPercentage.Text = "Win %";
            // 
            // colTotalScore
            // 
            this.colTotalScore.Text = "Total Score";
            this.colTotalScore.Width = 86;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Simulation Progress";
            // 
            // pbProgress
            // 
            this.pbProgress.Location = new System.Drawing.Point(6, 35);
            this.pbProgress.Name = "pbProgress";
            this.pbProgress.Size = new System.Drawing.Size(425, 23);
            this.pbProgress.TabIndex = 0;
            // 
            // SimulationForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(821, 330);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Name = "SimulationForm";
            this.Text = "SimulationForm";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.gbCards.ResumeLayout(false);
            this.gbPlayers.ResumeLayout(false);
            this.gbSimulation.ResumeLayout(false);
            this.gbSimulation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudGameCount)).EndInit();
            this.gbResults.ResumeLayout(false);
            this.gbResults.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox gbCards;
        private System.Windows.Forms.ListBox lbSelectedCards;
        private System.Windows.Forms.Button btnUnselectCard;
        private System.Windows.Forms.Button btnSelectCard;
        private System.Windows.Forms.ListBox lbAllCards;
        private System.Windows.Forms.GroupBox gbPlayers;
        private System.Windows.Forms.Button btnPlayerSettings;
        private System.Windows.Forms.Button btnRemovePlayer;
        private System.Windows.Forms.Button btnAddPlayer;
        private System.Windows.Forms.ListBox lbPlayers;
        private System.Windows.Forms.ComboBox cbPlayers;
        private System.Windows.Forms.GroupBox gbSimulation;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudGameCount;
        private System.Windows.Forms.GroupBox gbResults;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar pbProgress;
        private System.Windows.Forms.ListView lvResults;
        private System.Windows.Forms.ColumnHeader colPlayer;
        private System.Windows.Forms.ColumnHeader colWinPercentage;
        private System.Windows.Forms.ColumnHeader colTotalScore;
        private System.Windows.Forms.Button btnRandomCards;
        private System.Windows.Forms.TextBox txtOutputFilename;
        private System.Windows.Forms.Label label3;
    }
}