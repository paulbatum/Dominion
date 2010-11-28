using Dominion.Rules.Activities;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class SkipActionsBehaviour : IAIBehaviour
    {
        public bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return activity.ParseType() == ActivityType.PlayActions;
        }

        public void Respond(IGameClient client, ActivityModel activity, GameViewModel state)
        {
            var message = new MoveToBuyStepMessage(client.PlayerId);
            client.AcceptMessage(message);
        }
    }
}