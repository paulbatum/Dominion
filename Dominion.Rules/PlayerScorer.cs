using System.Linq;

namespace Dominion.Rules
{
    public class PlayerScorer
    {
        private ScoreZone _scoreZone;
        public string PlayerName { get; private set; }
        public PlayerScorer(Player player)
            : this(player.Name, player.Deck, player.Hand, player.Discards, player.PlayArea)
        {}

        public PlayerScorer(string playerName, params CardZone[] zones)
        {
            PlayerName = playerName;

            _scoreZone = new ScoreZone();

            foreach(var zone in zones)
                zone.MoveAll(_scoreZone);
        }

        public int Total
        {
            get { return _scoreZone.CalculateScore(); }
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