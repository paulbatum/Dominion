using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Rules.CardTypes;

namespace Dominion.Rules
{
    public class GameScores : Dictionary<Player, int>
    {
        private readonly Game _game;

        public GameScores(Game game)
        {
            _game = game;
        }

        public void Score(Player player)
        {
            player.Discards.MoveAll(player.PlayArea);
            player.Hand.MoveAll(player.PlayArea);
            player.Deck.MoveAll(player.PlayArea);

            player.PlayArea.SortForScoring();

            this[player] = player.PlayArea.OfType<IScoreCard>().Sum(c => c.Score(player.PlayArea));
        }

        public Player Winner
        {
            get
            {
                return _game.Players
                    .Last(player => this[player] == this.Values.Max());
            }
        }
    }
    
}