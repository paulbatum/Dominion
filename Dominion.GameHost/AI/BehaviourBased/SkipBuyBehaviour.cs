using Dominion.Rules.Activities;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class SkipBuyBehaviour : IAIBehaviour
    {
        public bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return activity.ParseType() == ActivityType.DoBuys;
        }

        public void Respond(IGameClient client, ActivityModel activity, GameViewModel state)
        {
            var message = new EndTurnMessage(client.PlayerId);
            client.AcceptMessage(message);
        }
    }
}