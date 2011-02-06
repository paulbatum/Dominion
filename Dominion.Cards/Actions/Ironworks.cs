using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Ironworks : Card, IActionCard
    {
        public Ironworks()
            : base(4)
        { }

        public void Play(TurnContext context)
        {
            var activity = Activities.GainACardCostingUpToX(context.Game.Log, context.ActivePlayer, 4, this);
            activity.AfterCardGained = card =>
            {
                if (card is IActionCard) context.RemainingActions += 1;
                if (card is ITreasureCard) context.AvailableSpend += 1;
                if (card is IVictoryCard) context.DrawCards(1);
            };

            context.AddSingleActivity(activity, this);
        }
    }
}