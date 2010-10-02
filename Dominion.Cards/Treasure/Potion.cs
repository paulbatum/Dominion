using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Treasure
{
    public class Potion : Card, ITreasureCard
    {
        public Potion() : base(4)
        {
        }

        public CardCost Value
        {
            get { return CardCost.Potion; }
        }
    }
}