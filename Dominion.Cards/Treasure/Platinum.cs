using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Treasure
{
    public class Platinum : Card, IMoneyCard
    {
        public Platinum()
            : base(9)
        { }

        public CardCost Value { get { return 5; } }
    }
}