using Dominion.Rules.CardTypes;
using Dominion.Rules;

namespace Dominion.Cards.Victory
{
    public class Province : Card, IVictoryCard
    {
        public Province()
            : base(8)
        {}

        public int Value
        {
            get { return 6; }
        }

        public int Score(EnumerableCardZone allCards)
        {
            return Value;
        }
    }
}