using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Courtyard : Card, IActionCard
    {
        public Courtyard() : base(2)
        {
        }

        public void Play(TurnContext context)
        {
            context.DrawCards(3);
            if (context.ActivePlayer.Hand.CardCount > 0)
                context.AddSingleActivity(Activities.PutCardFromHandOnTopOfDeck(context, this), this);
        }
        
    }
}