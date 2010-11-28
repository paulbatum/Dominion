using System.Linq;
using Dominion.Rules.Activities;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class DefaultSelectFromRevealedBehaviour : IAIBehaviour
    {
        public bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return activity.ParseType() == ActivityType.SelectFromRevealed;
        }

        public void Respond(IGameClient client, ActivityModel activity, GameViewModel state)
        {
            var selected = state.Revealed.First();
            client.AcceptMessage(new SelectCardsMessage(client.PlayerId, new[] { selected.Id }));
        }
    }
}