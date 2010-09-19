using Dominion.Rules.CardTypes;
using Dominion.Rules;

namespace Dominion.Cards.Treasure
{
    public class Gold : Card, IMoneyCard
    {
        public Gold()
            : base(6)
        { }

        public int Value { get { return 3; } }
    }
}