using System.Text;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Hybrid
{
    public class Harem : Card, IMoneyCard, IVictoryCard
    {
        public Harem() : base(6)
        {
            ScoringValue = 2;
        }

        public CardCost Value { get { return 2; } }

        public int ScoringValue
        {
            get;
            private set;
        }

        public int Score(EnumerableCardZone allCards)
        {
            return ScoringValue;
        }
    }
}
