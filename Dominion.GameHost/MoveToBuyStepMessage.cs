using System;
using Dominion.Rules;

namespace Dominion.GameHost
{
    public class MoveToBuyStepMessage : IGameActionMessage
    {
        public MoveToBuyStepMessage(Guid playerId)
        {
            PlayerId = playerId;
        }

        public Guid PlayerId { get; private set; }

        public void UpdateGameState(Game game)
        {
            game.CurrentTurn.MoveToBuyStep();            
        }

        public void Validate(Game game)
        {
            if (game.ActivePlayer.Id != PlayerId)
                throw new InvalidOperationException(string.Format("Player '{0}' is not active.", PlayerId));
        }
    }
}