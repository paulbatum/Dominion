using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class KingsCourt : Card, IActionCard
    {
        public KingsCourt() : base(7)
        {
        }

        public void Play(TurnContext context)
        {
            context.AddEffect(this, new KingsCourtEffect());
        }

        private class KingsCourtEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context, ICard source)
            {
                if (context.ActivePlayer.Hand.OfType<IActionCard>().Any())
                {
                    var activity = Activities.SelectActionToPlayMultipleTimes(context, context.ActivePlayer, context.Game.Log, source, 3);
                    _activities.Add(activity);
                }
                else
                {
                    context.Game.Log.LogMessage("{0} did not have any actions to use Kings Court on.", context.ActivePlayer.Name);
                }
            }
        }


    }
}