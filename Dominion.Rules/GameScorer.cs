using System.Linq;

namespace Dominion.Rules
{
    public class GameScorer
    {
        private ScoreZone _scoreZone;

        public GameScorer(Player player)
            : this(player.Deck, player.Hand, player.Discards, player.PlayArea)
        {}

        public GameScorer(params CardZone[] zones)
        {
            _scoreZone = new ScoreZone();

            foreach(var zone in zones)
                zone.MoveAll(_scoreZone);
        }

        public int CalculateScore()
        {
            return _scoreZone.CalculateScore();
        }

        private class ScoreZone : CardZone
        {
            public int CalculateScore()
            {
                return Cards.Sum(x => x.Score(this));
            }
        }
    }
}