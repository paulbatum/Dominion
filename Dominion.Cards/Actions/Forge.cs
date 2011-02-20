using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Forge : Card, IActionCard
    {
        public Forge()
            : base(7)
        {
        }

        public void Play(TurnContext context)
        {
            context.AddEffect(this, new ForgeEffect());
        }

        private class ForgeEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context, ICard source)
            {
                var cardCount = context.ActivePlayer.Hand.CardCount;
                
                if (cardCount == 0)
                {
                    AddGainActivityForCost(context, source, 0);
                }
                else
                {
                    var selectActivity = Activities.SelectUpToXCardsToTrash(context, context.ActivePlayer, cardCount, source);
                    selectActivity.AfterCardsSelected = cards =>
                    {
                        var costToGain = cards.Sum(x => x.Cost.Money);
                        context.TrashAll(context.ActivePlayer, cards);
                        AddGainActivityForCost(context, source, costToGain);                        
                    };

                    _activities.Add(selectActivity);
                }
            }

            private void AddGainActivityForCost(TurnContext context, ICard source, CardCost cost)
            {
                if (context.CanGainOfCost(cost))
                {
                    var gainActivity = Activities.GainACardCostingExactlyX(context.Game.Log, context.ActivePlayer,
                                                                           cost, context.ActivePlayer.Discards, source);
                    _activities.Add(gainActivity);
                }
                else
                {
                    context.Game.Log.LogMessage("{0} could gain no card of appropriate cost", context.ActivePlayer);
                }
            }
        }

       
    }
}