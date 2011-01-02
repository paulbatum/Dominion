using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class WorkersVillage : Card, IActionCard
    {
        public WorkersVillage()
            : base(4)
        { }

        public void Play(TurnContext context)
        {
            context.RemainingActions += 2;
            context.DrawCards(1);
            context.Buys += 1;
        }
    }
}