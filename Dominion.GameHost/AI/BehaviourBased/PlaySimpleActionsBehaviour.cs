using System;
using System.Linq;
using Dominion.Rules.Activities;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class PlaySimpleActionsBehaviour : IAIBehaviour
    {

        public bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return activity.ParseType() == ActivityType.PlayActions &&
                   state.Hand.Select(c => c.Name).Intersect(SimpleActions.All).Any();
        }

        public void Respond(IGameClient client, ActivityModel activity, GameViewModel state)
        {
            var action = state.Hand
                .Where(c => c.Is(CardType.Action))
                .OrderByDescending(c => SimpleActions.PlusActions.Contains(c.Name))
                .ThenByDescending(c => c.Cost)
                .First();

            var message = new PlayCardMessage(client.PlayerId, action.Id);
            client.AcceptMessage(message);
        }
    }
}