using Dominion.Cards.Treasure;
using Dominion.Rules;
using Dominion.Rules.Activities;
using Dominion.Rules.CardTypes;

namespace Dominion.Cards.Actions
{
    public class TradingPost : Card, IActionCard
    {
        public TradingPost() : base(5)
        {}

        public void Play(TurnContext context)
        {
            context.AddEffect(this, new TradingPostEffect());
        }

        public class TradingPostEffect : CardEffectBase
        {
            public override void Resolve(TurnContext context, ICard source)
            {
                var cardCount = context.ActivePlayer.Hand.CardCount;
                var gainUtil = new GainUtility(context, context.ActivePlayer);
                
                if(cardCount < 2)
                {
                    context.TrashAll(context.ActivePlayer, context.ActivePlayer.Hand);
                }
                else if(cardCount == 2)
                {
                    context.TrashAll(context.ActivePlayer, context.ActivePlayer.Hand);
                    gainUtil.Gain<Silver>(context.ActivePlayer.Hand);
                }
                else
                {
                    var activity = Activities.SelectXCardsToTrash(context, context.ActivePlayer, 2, source,
                                                                  () => gainUtil.Gain<Silver>(context.ActivePlayer.Hand));

                    _activities.Add(activity);
                }
            }
        }
    }
}