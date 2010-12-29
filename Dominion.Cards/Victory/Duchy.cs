using Dominion.Rules.CardTypes;
using Dominion.Rules;

namespace Dominion.Cards.Victory
{
    public class Duchy : Card, IVictoryCard
    {
        public Duchy()
            : base(5)
        {
            Value = 3;
        }

        public int Value
        {
            get;
            private set;
        }

        public int Score(EnumerableCardZone allCards)
        {
            return Value;
        }
    }
}