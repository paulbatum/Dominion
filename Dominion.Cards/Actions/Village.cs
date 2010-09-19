using Dominion.Rules;
using Dominion.Rules.CardTypes;
namespace Dominion.Cards.Actions
{
    public class Village : Card, IActionCard
    {
        public Village() : base(3)
        {}

        public void Play(TurnContext context)
        {
            context.RemainingActions += 2;
            context.DrawCards(1);
        }
    }
}