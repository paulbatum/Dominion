using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules.Activities;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class DefaultSelectFixedNumberOfCardsForPlayBehaviour : SelectFixedNumberOfCardsBehaviourBase
    {
        public bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return base.CanRespond(activity, state)
                   && activity.ParseHint() == ActivityHint.PlayCards
                   && state.Hand.Select(c => c.Name).Intersect(AISupportedActions.All).Any();
        }

        protected override IEnumerable<CardViewModel> PrioritiseCards(GameViewModel state, ActivityModel activity)
        {
            var actions = state.Hand
              .Where(c => c.Is(CardType.Action))
              .Where(c => AISupportedActions.All.Contains(c.Name))
              .OrderByDescending(c => c.Cost)
              .Take(activity.ParseNumberOfCardsToSelect());

            return actions;
        }       
    }
}
