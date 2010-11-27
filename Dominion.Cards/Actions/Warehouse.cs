using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Warehouse : Card, IActionCard
    {
        public Warehouse() : base(3)
        {
            
        }

        public void Play(TurnContext context)
        {
            context.RemainingActions += 1;
            context.DrawCards(3);
            context.AddEffect(this, new WarehouseEffect());
        }

        private class WarehouseEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context, ICard source)
            {
                if(context.ActivePlayer.Hand.CardCount < 4)
                {
                    context.DiscardCards(context.ActivePlayer, context.ActivePlayer.Hand);
                }
                else
                {
                    var discardActivity = Activities.DiscardCards(context, context.ActivePlayer, 3, source);
                    _activities.Add(discardActivity);    
                }
                
            }
        }
    }
}
