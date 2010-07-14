using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Woodcutter : ActionCard
    {
        public Woodcutter()
            : base(3)
        { }

        protected override void Play(TurnContext context)
        {
            context.MoneyToSpend += 2;
            context.Buys += 1;
        }
    }
}