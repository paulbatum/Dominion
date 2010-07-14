using Dominion.Rules;
using Dominion.Rules.CardTypes;
namespace Dominion.Cards.Actions
{
    public class Village : ActionCard
    {
        public Village() : base(3)
        {}

        protected override void Play(TurnContext context)
        {
            context.RemainingActions += 2;
            context.DrawCards(1);
        }
    }
}