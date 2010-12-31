using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class MerchantShip : Card, IDurationCard
    {
        public MerchantShip() : base(5)
        {}

        public void Play(TurnContext context)
        {
            context.AvailableSpend += 2;
            context.AddLongLivedEffect(new MerchantShipEffect(this));
        }

        public class MerchantShipEffect : DurationEffect
        {
            public MerchantShipEffect(ICard sourceCard) : base(sourceCard)
            {}

            public override void OnTurnStarting(TurnContext context)
            {
                base.OnTurnStarting(context);
                context.AvailableSpend += 2;
            }
        }
    }
}