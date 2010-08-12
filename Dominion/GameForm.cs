using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Dominion.GameHost;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion
{
    public partial class GameForm : Form
    {
        private Game _game;
        private IEnumerator<TurnContext> _turnEnumerator;

        public GameForm()
        {
            InitializeComponent();
            StartNewGame();
        }

        private void StartNewGame()
        {
            var host = new SolitaireHost();
            _game = host.CreateNewGame("Player");
            _turnEnumerator = _game.GameTurns().GetEnumerator();
            _turnEnumerator.MoveNext();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            RefreshUI();
        }

        private void RefreshUI()
        {
            var turn = _turnEnumerator.Current;
            UpdateHand(turn);
            UpdateTurnStats(turn);
            UpdateBank(_game.Bank);

            btnBuyStep.Enabled = !turn.InBuyStep;                
        }

        private void UpdateBank(CardBank bank)
        {
            var container = flowLayoutPanel1;
            container.Controls.Clear();

            foreach(var pile in bank.Piles)
            {
                var button = new Button();
                button.Width = 200;

                button.Enabled = _turnEnumerator.Current.InBuyStep
                                 && _turnEnumerator.Current.Buys > 0
                                 && _turnEnumerator.Current.MoneyToSpend >= pile.TopCard.Cost
                                 && !pile.IsEmpty;

                var currentPile = pile;

                if(pile is UnlimitedSupplyCardPile)
                {
                    button.Text = string.Format("Buy {0} for {1} ", pile.TopCard, pile.TopCard.Cost);
                    button.Click += (sender, args) => { _turnEnumerator.Current.Buy(currentPile); RefreshUI(); };
                }
                else if(pile.IsEmpty)
                {
                    button.Text = string.Format("Sold out");
                    button.Enabled = false;
                }
                else
                {
                    button.Text = string.Format("Buy {0} for {1} ({2} remaining)", pile.TopCard, pile.TopCard.Cost, pile.CardCount);
                    button.Click += (sender, args) => { _turnEnumerator.Current.Buy(currentPile); RefreshUI(); };
                }

                container.Controls.Add(button);
            }
        }

        private void UpdateHand(TurnContext turn)
        {
            lbHand.Items.Clear();
            foreach (var card in turn.Player.Hand)
                lbHand.Items.Add(card);
        }

        private void UpdateTurnStats(TurnContext turn)
        {
            lblActions.Text = turn.RemainingActions.ToString();
            lblBuyOf.Text = turn.MoneyToSpend.ToString();
            lblBuys.Text = turn.Buys.ToString();
        }

        private void btnEndTurn_Click(object sender, EventArgs e)
        {
            _turnEnumerator.Current.EndTurn();
            if (_turnEnumerator.MoveNext() == false)
            {
                var scorer = _game.ActivePlayer.CreateScorer();
                MessageBox.Show("The game is over. Score: " + scorer.CalculateScore());
                StartNewGame();
            }

            RefreshUI();
        }

        private void lbHand_DoubleClick(object sender, EventArgs e)
        {
            var card = lbHand.SelectedItem as ActionCard;
            var turn = _turnEnumerator.Current;

            if (card != null)
            {
                if (turn.CanPlay(card))
                {
                    turn.Play(card);
                    RefreshUI();
                }
                else
                {
                    MessageBox.Show("You've run out of actions!");
                }
            }
        }

        private void btnBuyStep_Click(object sender, EventArgs e)
        {
            var turn = _turnEnumerator.Current;
            turn.MoveToBuyStep();            
            RefreshUI();
        }
    }
}
