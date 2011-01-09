using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Apprentice : Card, IActionCard
    {
        public Apprentice()
            : base(5)
        {
        }

        public void Play(TurnContext context)
        {
            context.RemainingActions += 1;
            context.AddEffect(this, new ApprenticeEffect());
        }

        private class ApprenticeEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context, ICard source)
            {
                if (context.ActivePlayer.Hand.CardCount > 0)
                {
                    var activity = new SelectCardsActivity(context, "Select a card to trash",
                                                           SelectionSpecifications.SelectExactlyXCards(1), source);

                    activity.AfterCardsSelected = cardList =>
                    {
                        var cardToTrash = cardList.Single();
                        var cardsToDraw = cardToTrash.Cost.Money;

                        // Notice the card doesn't say its 2 per potion (if we add cards that cost multiple potions)
                        if (cardToTrash.Cost.Potions > 0)
                            cardsToDraw += 2;

                        context.DrawCards(cardsToDraw);
                        context.Trash(context.ActivePlayer, cardToTrash);
                    };
                    _activities.Add(activity);
                }
            }
        }
    }
}