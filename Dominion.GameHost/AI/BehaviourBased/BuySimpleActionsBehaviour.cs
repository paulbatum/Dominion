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
                   GetValidBuys(state).Any(pile => AISupportedActions.All.Contains(pile.Name));
        }

        protected override CardPileViewModel SelectPile(GameViewModel state, IGameClient client)
        {
            var options = GetValidBuys(state)
                .Where(pile => AISupportedActions.All.Contains(pile.Name))
                .OrderByDescending(pile => pile.Cost)
                .ThenBy(pile => _random.Next(100));

            var message = string.Format("I considered {0}.", string.Join(", ", options.Select(x => x.Name).ToArray()));
            client.SendChatMessage(message);

            return options.First();
        }

        
    }
}