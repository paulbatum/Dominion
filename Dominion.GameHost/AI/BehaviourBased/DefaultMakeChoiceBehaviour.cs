using System.Linq;
using Dominion.Rules.Activities;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class DefaultMakeChoiceBehaviour : IAIBehaviour
    {
        public virtual bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return activity.ParseType() == ActivityType.MakeChoice;
        }

        public void Respond(IGameClient client, ActivityModel activity, GameViewModel state)
        {
            var options = activity.ParseOptions();

            Choice choice = options.First();

            if (options.Contains(Choice.Yes))
                choice = Choice.Yes;

            client.AcceptMessage(new ChoiceMessage(client.PlayerId, choice.ToString()));
        }
    }
}