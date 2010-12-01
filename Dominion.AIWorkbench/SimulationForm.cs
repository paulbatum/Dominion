using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Concurrency;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Dominion.GameHost;
using Dominion.GameHost.AI;
using Dominion.GameHost.AI.BehaviourBased;
using Dominion.Rules;

namespace Dominion.AIWorkbench
{
    public partial class SimulationForm : Form
    {
        private readonly int _simulationNumber;
        private Simulation _simulation;
        private List<Type> _aiTypes;

        public SimulationForm(int simulationNumber)
        {
            _simulationNumber = simulationNumber;            
            _simulation = new Simulation();
            _aiTypes = new List<Type>();

            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Text = "Simulation " + _simulationNumber;
            txtOutputFilename.Text = Text;
            LoadAIs();
            LoadCards();
        }

        private void LoadCards()
        {
            var cardList = CardFactory.OptionalCardsForBank
                .OrderBy(c => c)
                .ToArray();

            lbAllCards.Items.Clear();
            lbAllCards.Items.AddRange(cardList);
        }

        private void LoadAIs()
        {
            _aiTypes = typeof(SimpleAI).Assembly
                .GetExportedTypes()
                .Where(t => t.IsSubclassOf(typeof(BaseAIClient)))
                .Where(t => !t.IsAbstract)
                .ToList();                
           
            cbPlayers.Items.Clear();
            cbPlayers.Items.AddRange(_aiTypes.Select(t => t.Name).ToArray());
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            var players = new Dictionary<string, Type>();
            for (int i = 0; i < lbPlayers.Items.Count; i++)
            {
                var typeName = (string) lbPlayers.Items[i];
                var playerName = string.Format("Player {0} - {1}", i + 1, typeName);
                players[playerName] = _aiTypes.Single(t => t.Name == typeName);
            }

            if (!Directory.Exists(txtOutputFilename.Text))
                Directory.CreateDirectory(txtOutputFilename.Text);

            _simulation.Name = txtOutputFilename.Text;
            _simulation.Cards = lbSelectedCards.Items.Cast<string>().ToList();
            _simulation.Players = players;
            _simulation.NumberOfGamesToExecute = (int) nudGameCount.Value;
            pbProgress.Maximum = (int)nudGameCount.Value;
            _simulation.Run(UpdateResults, OnDone);
        }

        private void btnSelectCard_Click(object sender, EventArgs e)
        {
            foreach (var item in lbAllCards.SelectedItems.Cast<string>().ToList())
            {
                lbAllCards.Items.Remove(item);
                lbSelectedCards.Items.Add(item);                
            }            
        }

        private void btnUnselectCard_Click(object sender, EventArgs e)
        {
            foreach (var item in lbSelectedCards.SelectedItems.Cast<string>().ToList())
            {
                lbAllCards.Items.Add(item);
                lbSelectedCards.Items.Remove(item);                
            }
        }

        private void OnDone(Task t)
        {
            this.Cursor = Cursors.Default;
        }

        private void UpdateResults(Task<ResultsSummary> task)
        {
            task.Wait();
            var result = task.Result;
            
            lvResults.Items.Clear();
            foreach (var gameResult in result.Results)
            {
                lvResults.Items.Add(
                    new ListViewItem(new[] { gameResult.PlayerName, gameResult.WinPercentage.ToString(), gameResult.TotalScore.ToString() }));
            }

            pbProgress.Value = result.CompletedGameCount;
        }

        private void btnAddPlayer_Click(object sender, EventArgs e)
        {
            var item = cbPlayers.SelectedItem;
            if(item != null)
            {                
                lbPlayers.Items.Add(item);
            }
        }

        private void btnRemovePlayer_Click(object sender, EventArgs e)
        {
            var item = lbPlayers.SelectedItem;
            if (item != null)
            {                
                lbPlayers.Items.Remove(item);
            }
        }

        private void btnRandomCards_Click(object sender, EventArgs e)
        {
            var random = new Random();

            10.Times(() =>
            {
                lbAllCards.SelectedIndex = random.Next(1, lbAllCards.Items.Count);
                btnSelectCard_Click(null, EventArgs.Empty);
            });
        }

        private void lbAllCards_DoubleClick(object sender, EventArgs e)
        {
            btnSelectCard_Click(sender, e);
        }

        private void lbSelectedCards_DoubleClick(object sender, EventArgs e)
        {
            btnUnselectCard_Click(sender, e);
        }


    }
}
