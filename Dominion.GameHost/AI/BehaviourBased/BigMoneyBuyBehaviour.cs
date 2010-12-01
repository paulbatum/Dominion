using System.Linq;
using Dominion.Cards.Treasure;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class BigMoneyBuyBehaviour : BuyBehaviourBase
    {
        protected override CardPileViewModel SelectPile(GameViewModel state)
        {
            return GetValidBuys(state)                
                .Where(pile => Treasure.Basic.Contains(pile.Name))                
                .OrderByDescending(pile => pile.Cost)
                .First();
        }

    }
}