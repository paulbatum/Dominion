using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Feast : ActionCard
    {
        public Feast() : base(4) { }

        protected override void Play(TurnContext context)
        {
            context.Trash(context.ActivePlayer, this);
            context.AddEffect(new FeastEffect(context));
        }
    }

    public class FeastEffect : CardEffectBase
    {
        public FeastEffect(TurnContext context)
        {
            _activities.Add(new GainACardUpToActivity(context.Game.Log, context.ActivePlayer, 5));
        }
    }
}
