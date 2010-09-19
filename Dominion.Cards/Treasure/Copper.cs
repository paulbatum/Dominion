using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Treasure
{
    public class Copper : Card, IMoneyCard
    {
        public Copper() : base(0)
        {}

        public int Value { get { return 1; } }
    }
}