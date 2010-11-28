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

        protected override void TalkSmack(CardPileViewModel pile, IGameClient client)
        {
            base.TalkSmack(pile, client);

            if (pile.Name == "Province")
                client.SendChatMessage("Province muthafucka!");

            if (pile.Name == "Colony")
                client.SendChatMessage("COLONY! SUCK IT!");
        }
    }
}