using System.Collections.Generic;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Militia : ActionCard
    {
        public Militia() : base(4)
        {
        }

        protected override void Play(TurnContext context)
        {
            context.MoneyToSpend += 2;
            context.AddEffect(new MilitiaEffect(context));
        }

        private class MilitiaEffect : CardEffectBase
        {
            public MilitiaEffect(TurnContext context)
            {
                foreach (var player in context.Opponents)
                {
                    if (player.Hand.OfType<Moat>().Any())
                    {
                        context.Game.Log.LogMoat(player);
                        continue;
                    }

                    var numberToDiscard = player.Hand.CardCount - 3;

                    if(numberToDiscard > 0)
                        _activities.Add(new DiscardCardsActivity(context.Game.Log, player, numberToDiscard));
                    else
                    {
                        context.Game.Log.LogMessage("{0} did not have to discard any cards", player.Name);
                    }
                }                    
            }
        }
    }
}