using System.Linq;
using Dominion.Cards.Treasure;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class BigMoneyBuyBehaviour : BuyBehaviourBase
    {
        protected override CardPileViewModel SelectPile(GameViewModel state)
        {
            return GetValidBuys(state)
                .Where(pile => pile.Is<Potion>() == false) // Stupid potions! "Smarter" big money was too dumb to ignore them!
                .OrderByDescending(pile => pile.Is(CardType.Treasure))                
                .ThenByDescending(pile => pile.Cost)
                .First();
        }


    }
}