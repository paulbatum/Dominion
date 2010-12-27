using System.Linq;
using Dominion.Cards.Treasure;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class BuyPotionBehaviour : BuyBehaviourBase
    {
        public override bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            var potionPile = state.Bank.SingleOrDefault(p => p.Is<Potion>());
            return base.CanRespond(activity, state)
                   && potionPile != null
                   && potionPile.CanBuy
                   && state.Status.AvailableSpend.Money == potionPile.Cost;
        }

        protected override CardPileViewModel SelectPile(GameViewModel state, IGameClient client)
        {
            return state.Bank.Single(p => p.Is<Potion>());
        }
    }
}