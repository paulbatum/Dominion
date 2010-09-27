using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dominion.Rules;
using Dominion.Rules.Activities;

namespace Dominion.GameHost
{
    public class ChoiceMessage : IGameActionMessage
    {
        public ChoiceMessage(Guid playerId, string choice)
        {
            PlayerId = playerId;
            Choice = choice;
        }

        public Guid PlayerId { get; private set; }
        public string Choice { get; set; }

        public void UpdateGameState(Game game)
        {
            var player = game.Players.Single(p => p.Id == PlayerId);
            var activity = game.GetPendingActivity(player) as ChoiceActivity;

            if (activity == null)
                throw new InvalidOperationException("There must be a corresponding activity");

            activity.MakeChoice(Choice);
        }

        public void Validate(Game game)
        {

        }
    }
}
