using System.Linq;
using Dominion.Cards.Curses;
using Dominion.Cards.Treasure;
using Dominion.Cards.Victory;
using Dominion.Rules.Activities;

namespace Dominion.GameHost.AI.BehaviourBased
{
    public class DefaultSelectUpToNumberOfCardsToTrashBehaviour : IAIBehaviour
    {
        public virtual bool CanRespond(ActivityModel activity, GameViewModel state)
        {
            return activity.ParseType() == ActivityType.SelectUpToNumberOfCards
                   && activity.ParseHint() == ActivityHint.TrashCards;
        }

        public void Respond(IGameClient client, ActivityModel activity, GameViewModel state)
        {
            var idsToTrash = state.Hand.Where(c => c.Is<Curse>() || c.Is<Copper>() || c.Is<Estate>())
                .Select(c => c.Id)
                .Take(activity.ParseNumberOfCardsToSelect())
                .ToArray();

            client.AcceptMessage(new SelectCardsMessage(client.PlayerId, idsToTrash));
        }
    }
}