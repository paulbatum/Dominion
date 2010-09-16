using System;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.Activities;

namespace Dominion.GameHost
{
    public class YesNoChoiceMessage : IGameActionMessage
    {
        public YesNoChoiceMessage(Guid playerId, bool choice)
        {
            PlayerId = playerId;
            Choice = choice;
        }

        public Guid PlayerId { get; private set; }
        public bool Choice { get; set; }

        public void UpdateGameState(Game game)
        {
            var player = game.Players.Single(p => p.Id == PlayerId);
            var activity = game.GetPendingActivity(player) as YesNoChoiceActivity;

            if (activity == null)
                throw new InvalidOperationException("There must be a corresponding activity");

            activity.MakeChoice(Choice);
        }

        public void Validate(Game game)
        {
            
        }
    }
}