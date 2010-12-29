using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Victory
{
    public class Colony : Card, IVictoryCard
    {
        public Colony()
            : base(11)
        { }

        public int Value
        {
            get { return 10; }
        }

        public int Score(EnumerableCardZone allCards)
        {
            return Value;
        }
    }
}