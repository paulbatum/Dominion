using System;
using System.Collections.Generic;
using System.Linq;
using Dominion.Cards.Victory;
using Dominion.Rules.Activities;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class DefaultSelectFixedNumberOfCardsToPassOrTrashBehaviour : SelectFixedNumberOfCardsBehaviourBase
    {
        public override bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return base.CanRespond(activity, state) && 
                (activity.ParseHint() == ActivityHint.PassCards || activity.ParseHint() == ActivityHint.TrashCards);
        }

        protected override IEnumerable<CardViewModel> PrioritiseCards(GameViewModel state, ActivityModel activity)
        {
            return state.Hand
                .OrderByDescending(c => c.Is(CardType.Curse))
                .ThenByDescending(c => c.Is<Estate>())
                .ThenBy(c => c.Cost);
        }
    }
}