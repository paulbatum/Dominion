using System;

namespace Dominion.Rules
{
    public class TurnContext
    {
        public TurnContext(Player player)
        {
            _player = player;
            MoneyToSpend = 0;
            RemainingActions = 1;
        }

        private Player _player;
        public int MoneyToSpend { get; set; }
        public int RemainingActions { get; set; }


        public void DrawCards(int numberOfCardsToDraw)
        {
            _player.Deck.MoveCards(_player.Hand, numberOfCardsToDraw);
        }
    }
}