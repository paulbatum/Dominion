using System.Linq;
using Dominion.Cards.Treasure;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class BigMoneyBuyBehaviour : BuyBehaviourBase
    {
        public override bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return base.CanRespond(activity, state) &&             
                GetValidBuys(state)                
                    .Any(pile => Treasure.Basic.Contains(pile.Name));
        }

        protected override CardPileViewModel SelectPile(GameViewModel state)
        {
            return GetValidBuys(state)                
                .Where(pile => Treasure.Basic.Contains(pile.Name))                
                .OrderByDescending(pile => pile.Cost)
                .First();
        }

    }
}