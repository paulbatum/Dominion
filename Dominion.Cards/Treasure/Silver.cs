using Dominion.Rules.CardTypes;
using Dominion.Rules;

namespace Dominion.Cards.Treasure
{
    public class Silver : Card, IMoneyCard
    {
        public Silver()
            : base(3)
        { }

        public int Value { get { return 2; } }
    }
}