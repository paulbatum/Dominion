using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class Wharf : Card, IDurationCard
    {
        public Wharf()
            : base(5)
        { }

        public void Play(TurnContext context)
        {
            context.DrawCards(2);
            context.Buys += 1;
            context.AddLongLivedEffect(new WharfEffect(this));
        }

        public class WharfEffect : DurationEffect
        {
            public WharfEffect(ICard sourceCard)
                : base(sourceCard)
            { }

            public override void OnTurnStarting(TurnContext context)
            {
                base.OnTurnStarting(context);
                context.DrawCards(2);
                context.Buys += 1;
            }
        }
    }
}