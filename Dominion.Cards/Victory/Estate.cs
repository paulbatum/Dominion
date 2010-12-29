using Dominion.Rules.CardTypes;
using Dominion.Rules;
namespace Dominion.Cards.Victory
{
    public class Estate : Card, IVictoryCard
    {
        public Estate() : base(2)
        {
            Value = 1;
        }

        public int Value
        {
            get;private set;
        }

        public int Score(EnumerableCardZone allCards)
        {
            return Value;
        }
    }
}