using System.Collections.Generic;
using System.Linq;
using Dominion.Cards.Victory;
using Dominion.Rules.Activities;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class DefaultPassCardsBehaviour : SelectFixedNumberOfCardsBehaviourBase
    {
        public override bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return base.CanRespond(activity, state) && activity.ParseHint() == ActivityHint.PassCards;
        }

        protected override IEnumerable<CardViewModel> PrioritiseCards(GameViewModel state)
        {
            return state.Hand
                .OrderByDescending(c => c.Is(CardType.Curse))
                .ThenByDescending(c => c.Is<Estate>())
                .ThenBy(c => c.Cost);
        }
    }
}