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

            var host = new SolitaireHost();
            _game = host.CreateNewGame("Player");
            _turnEnumerator = _game.GameTurns().GetEnumerator();
            
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            _turnEnumerator.MoveNext();
            RefreshUI();
        }

        private void RefreshUI()
        {
            var turn = _turnEnumerator.Current;
            UpdateHand(turn);
            UpdateTurnStats(turn);
            UpdateBank(_game.Bank);
        }

        private void UpdateBank(CardBank bank)
        {
            var container = flowLayoutPanel1;
            container.Controls.Clear();

            foreach(var pile in bank.Piles)
            {
                var button = new Button();
                button.Width = 200;

                button.Enabled = _turnEnumerator.Current.InBuyStep && _turnEnumerator.Current.Buys > 0;

                var currentPile = pile;

                if(pile is UnlimitedSupplyCardPile)
                {
                    button.Text = "Buy " + pile.TopCard;
                    button.Click += (sender, args) => { _turnEnumerator.Current.Buy(currentPile.TopCard); RefreshUI(); };
                }
                else if(pile.IsEmpty)
                {
                    button.Text = string.Format("Sold out");

                }
                else
                {
                    button.Text = string.Format("Buy {0} ({1} remaining)", pile.TopCard, pile.CardCount);
                    button.Click += (sender, args) => { _turnEnumerator.Current.Buy(currentPile.TopCard); RefreshUI(); };
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
            _turnEnumerator.MoveNext();
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
