using System;
using Dominion.Rules;

namespace Dominion.GameHost
{
    public class EndTurnMessage : IGameActionMessage
    {
        public EndTurnMessage(Guid playerId)
        {
            PlayerId = playerId;
        }

        public Guid PlayerId { get; private set; }

        public void UpdateGameState(Game game)
        {
            game.EndTurn();            
        }

        public void Validate(Game game)
        {
            if (game.ActivePlayer.Id != PlayerId)
                throw new InvalidOperationException(string.Format("Player '{0}' is not active.", PlayerId));
        }
    }
}