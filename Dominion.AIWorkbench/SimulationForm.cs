using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Concurrency;
using System.Data;
using System.Drawing;
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
        private Simulation _simulation;
        private List<Type> _aiTypes;

        public SimulationForm()
        {
            InitializeComponent();
            _simulation = new Simulation();
            _aiTypes = new List<Type>();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
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
            var players = new Dictionary<string, Type>();
            for (int i = 0; i < lbPlayers.Items.Count; i++)
            {
                var typeName = (string) lbPlayers.Items[i];
                var playerName = string.Format("Player {0} - {1}", i + 1, typeName);
                players[playerName] = _aiTypes.Single(t => t.Name == typeName);
            }

            _simulation.Cards = lbSelectedCards.Items.Cast<string>().ToList();
            _simulation.Players = players;
            _simulation.NumberOfGamesToExecute = (int) nudGameCount.Value;
            pbProgress.Maximum = (int)nudGameCount.Value;
            _simulation.Run(UpdateResults);
        }

        private void btnSelectCard_Click(object sender, EventArgs e)
        {
            var item = lbAllCards.SelectedItem;
            if (item != null)
            {
                lbAllCards.Items.Remove(item);
                lbSelectedCards.Items.Add(item);                
            }            
        }

        private void btnUnselectCard_Click(object sender, EventArgs e)
        {
            var item = lbAllCards.SelectedItem;
            if (item != null)
            {
                lbAllCards.Items.Add(item);
                lbSelectedCards.Items.Remove(item);                
            }
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
            var item = cbPlayers.SelectedItem;
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


    }
}
