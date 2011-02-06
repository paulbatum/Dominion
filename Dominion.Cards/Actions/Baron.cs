using System;
using System.Linq;
using Dominion.Cards.Victory;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Baron : Card, IActionCard
    {
        public Baron()
            : base(4)
        { }

        public void Play(TurnContext context)
        {
            context.AddEffect(this, new BaronEffect());
            context.Buys += 1;
        }

        public class BaronEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context, ICard source)
            {
                var estate = context.ActivePlayer.Hand.OfType<Estate>().FirstOrDefault();

                Action discardAction = () =>
                {
                    context.AvailableSpend += 4;
                    context.DiscardCard(context.ActivePlayer, estate);
                };

                Action gainAction = () => new GainUtility(context, context.ActivePlayer).Gain<Estate>();

                if (estate != null)
                {
                    var activity = Activities.ChooseYesOrNo
                        (context.Game.Log, context.ActivePlayer, "Discard an Estate?",
                         source,
                         discardAction, gainAction);

                    _activities.Add(activity);
                }
                else
                {
                    gainAction();
                }
            }
        }
    }
}