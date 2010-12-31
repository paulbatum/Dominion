using Dominion.Cards.Treasure;
using Dominion.Cards.Victory;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Transmute : Card, IActionCard
    {
        public Transmute() : base(CardCost.Parse("0P"))
        {}

        public void Play(TurnContext context)
        {
            if(context.ActivePlayer.Hand.CardCount > 0)
            {
                var gainUtility = new GainUtility(context, context.ActivePlayer);

                var activity = Activities.SelectACardToTrash(context, context.ActivePlayer, this, card =>
                {
                    if (card is IActionCard) gainUtility.Gain<Duchy>();
                    if (card is ITreasureCard) gainUtility.Gain<Transmute>();
                    if (card is IVictoryCard) gainUtility.Gain<Gold>();
                });

                context.AddSingleActivity(activity, this);
            }
        }
    }
}