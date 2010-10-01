using System;
using System.Linq;
using Dominion.Rules;
using Dominion.Rules.CardTypes;

namespace Dominion.GameHost
{
    public class PlayCardMessage : IGameActionMessage
    {
        public PlayCardMessage(Guid playerId, Guid cardId)
        {
            PlayerId = playerId;
            CardId = cardId;
        }

        public Guid PlayerId { get; private set; }
        public Guid CardId { get; private set; }

        public void UpdateGameState(Game game)
        {
            ICard card = game.CurrentTurn.ActivePlayer.Hand.Single(c => c.Id == CardId);
            game.CurrentTurn.Play((IActionCard) card);            
        }

        public void Validate(Game game)
        {
            if (game.ActivePlayer.Id != PlayerId)
                throw new InvalidOperationException(string.Format("Player '{0}' is not active.", PlayerId));
        }
    }
}