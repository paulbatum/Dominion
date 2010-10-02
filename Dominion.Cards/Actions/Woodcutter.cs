using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Woodcutter : Card, IActionCard
    {
        public Woodcutter()
            : base(3)
        { }

        public void Play(TurnContext context)
        {
            context.AvailableSpend += 2;
            context.Buys += 1;
        }
    }
}