using System.Linq;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Victory
{
    public class Duke : Card, IVictoryCard
    {
        public Duke() : base(5)
        {
        }

        public int Score(EnumerableCardZone allCards)
        {
            return allCards.OfType<Duchy>().Count();
        }
    }
}