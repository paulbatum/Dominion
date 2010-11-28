using System;
using System.Linq;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class BuySimpleActionsBehaviour : BuyBehaviourBase
    {
        private readonly Random _random = new Random();

        public override bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return base.CanRespond(activity, state) &&
                   GetValidBuys(state).Any(pile => SimpleActions.All.Contains(pile.Name));
        }

        protected override CardPileViewModel SelectPile(GameViewModel state)
        {
            return GetValidBuys(state)
                .Where(pile => SimpleActions.All.Contains(pile.Name))
                .OrderByDescending(pile => pile.Cost)
                .ThenBy(pile => _random.Next(100))
                .First();
        }
    }
}