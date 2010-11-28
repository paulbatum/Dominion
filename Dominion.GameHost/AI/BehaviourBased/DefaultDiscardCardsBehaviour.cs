using System.Collections.Generic;
using System.Linq;
using Dominion.Rules.Activities;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class DefaultDiscardCardsBehaviour : SelectFixedNumberOfCardsBehaviourBase
    {
        public override bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return base.CanRespond(activity, state) && activity.ParseHint() == ActivityHint.DiscardCards;
        }

        protected override IEnumerable<CardViewModel> PrioritiseCards(GameViewModel state)
        {
            return state.Hand
                .OrderByDescending(c => c.Is(CardType.Treasure) == false)
                .ThenByDescending(c => c.Is(CardType.Action) == false)
                .ThenBy(c => c.Cost);
        }
    }
}