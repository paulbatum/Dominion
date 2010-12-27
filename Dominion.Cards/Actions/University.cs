using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class University : Card, IActionCard
    {
        public University() : base(CardCost.Parse("2P"))
        {
        }

        public void Play(TurnContext context)
        {
            context.RemainingActions += 2;
            context.AddSingleActivity(
                Activities.GainAnActionCardCostingUpToX(context.Game.Log, context.ActivePlayer, 5, this, true), this);
        }
    }
}