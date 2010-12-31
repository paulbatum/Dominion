using System.Linq;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Victory
{
    public class Vineyard : Card, IVictoryCard
    {
        public Vineyard() : base(CardCost.Parse("0P"))
        {}

        public int Score(EnumerableCardZone allCards)
        {
            return allCards.OfType<IActionCard>().Count() / 3;
        }
    }
}