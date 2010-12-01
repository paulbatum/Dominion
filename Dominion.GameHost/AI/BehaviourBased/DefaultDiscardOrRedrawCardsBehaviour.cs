using System.Collections.Generic;
using System.Linq;
using Dominion.Rules.Activities;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class DefaultDiscardOrRedrawCardsBehaviour : SelectFixedNumberOfCardsBehaviourBase
    {
        public override bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return base.CanRespond(activity, state) && 
                (activity.ParseHint() == ActivityHint.DiscardCards || activity.ParseHint() == ActivityHint.RedrawCards);
        }

        protected override IEnumerable<CardViewModel> PrioritiseCards(GameViewModel state, ActivityModel activity)
        {
            return state.Hand
                .OrderByDescending(c => c.Is(CardType.Treasure) == false)
                .ThenByDescending(c => c.Is(CardType.Action) == false)
                .ThenBy(c => c.Cost);
        }
    }
}