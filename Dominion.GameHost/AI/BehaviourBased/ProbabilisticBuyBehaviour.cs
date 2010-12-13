using System;
using System.Linq;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class ProbabilisticBuyBehaviour : BuyBehaviourBase
    {
        private ProbabilityDistribution _distribution;

        public ProbabilisticBuyBehaviour()
        {
            _distribution = new ProbabilityDistribution(AISupportedActions.All, Treasure.Basic);
        }

        public override bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return base.CanRespond(activity, state) && GetValidBuys(state)
                                                           .Any(c => _distribution.Contains(c.Name));
        }

        protected override CardPileViewModel SelectPile(GameViewModel state)
        {
            var validBuys = GetValidBuys(state);
            return _distribution.RandomItem(validBuys, c => c.Name);
        }

        
    }
}