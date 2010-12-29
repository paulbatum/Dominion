using System;
using System.Linq;
using Dominion.Cards.Treasure;
using Dominion.Cards.Victory;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Explorer : Card, IActionCard
    {
        public Explorer()
            : base(5)
        {
        }

        public void Play(TurnContext context)
        {
            var gainUtility = new GainUtility(context, context.ActivePlayer);
            Action gainSilver = () => gainUtility.Gain<Silver>(context.ActivePlayer.Hand);
            Action gainGold = () => gainUtility.Gain<Gold>(context.ActivePlayer.Hand);

            if (context.ActivePlayer.Hand.OfType<Province>().Any())
            {
                var activity = Activities.ChooseYesOrNo(
                    context.Game.Log,
                    context.ActivePlayer,
                    "Reveal a Province to gain a Gold?",
                    this,
                    gainGold,
                    gainSilver);
                context.AddSingleActivity(activity, this);
            }
            else
            {
                gainSilver();
            }
        }
    }
}